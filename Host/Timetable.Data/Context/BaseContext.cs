using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Timetable.Base.Entities;
using Timetable.Data.Context.Interfaces;

namespace Timetable.Data.Context
{
    public abstract class BaseContext : DbContext, IDataContext
    {
        /// <summary>
        /// Return models error
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetValidationModelErrors()
        {
            return GetValidationErrors();
        }

        protected BaseContext(string connectionStringOrName) : base(connectionStringOrName) { }

        protected BaseContext() {}

        protected BaseContext(DbConnection connection, bool contextOwnsConnection) : base(connection, contextOwnsConnection) { }

        protected virtual void Initialize()
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }

        public new IDbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

        public virtual void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            AttachIfNotAttached(entity);
            Set(entity.GetType()).Add(entity);
            SaveChanges();
        }

        public virtual void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            AttachIfNotAttached(entity);
            Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            AttachIfNotAttached(entity);
            Set(entity.GetType()).Remove(entity);
            Entry(entity).State = EntityState.Deleted;
            SaveChanges();
        }

        public void AttachIfNotAttached<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (Entry(entity).State != EntityState.Detached)
                return;
            Set(entity.GetType()).Attach(entity);
        }

        public IQueryable<TEntity> RawSqlQuery<TEntity>(
            string query,
            params object[] parameters) where TEntity : BaseEntity
        {
            var result = base.Set<TEntity>().SqlQuery(query, parameters).AsQueryable();

            return result;
        }

        public int RawSqlCommand(
            string command,
            params object[] parameters)
        {
            return Database.ExecuteSqlCommand(command, parameters);
        }

        public IQueryable<dynamic> RawSqlQuery(
            List<Type> types,
            List<string> names,
            string query,
            params object[] parameters)
        {
            var builder = CreateTypeBuilder("MyDynamicAssembly", "MyModule", "MyType");

            var typesAndNames = types.Zip(names, (t, n) => new { Type = t, Name = n });
            foreach (var tn in typesAndNames)
            {
                CreateAutoImplementedProperty(builder, tn.Name, tn.Type);
            }

            var resultType = builder.CreateType();

            return Database.SqlQuery(resultType, query, parameters).Cast<dynamic>().AsQueryable();
        }


        private static TypeBuilder CreateTypeBuilder(
           string assemblyName,
            string moduleName,
            string typeName)
        {
            var typeBuilder = AppDomain
                .CurrentDomain
                .DefineDynamicAssembly(
                    new AssemblyName(assemblyName),
                    AssemblyBuilderAccess.Run)
                .DefineDynamicModule(moduleName)
                .DefineType(typeName, TypeAttributes.Public);

            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            return typeBuilder;
        }

        private static void CreateAutoImplementedProperty(
            TypeBuilder builder,
            string propertyName,
            Type propertyType)
        {
            const string privateFieldPrefix = "m_";
            const string getterPrefix = "get_";
            const string setterPrefix = "set_";

            // Generate the field.
            var fieldBuilder = builder.DefineField(
                string.Concat(privateFieldPrefix, propertyName),
                              propertyType, FieldAttributes.Private);

            // Generate the property
            var propertyBuilder = builder.DefineProperty(
                propertyName, System.Reflection.PropertyAttributes.HasDefault, propertyType, null);

            // Property getter and setter attributes.
            const MethodAttributes propertyMethodAttributes = MethodAttributes.Public
                                                              | MethodAttributes.SpecialName
                                                              | MethodAttributes.HideBySig;

            // Define the getter method.
            var getterMethod = builder.DefineMethod(
                string.Concat(getterPrefix, propertyName),
                propertyMethodAttributes, propertyType, Type.EmptyTypes);

            // Emit the IL code.
            // ldarg.0
            // ldfld,_field
            // ret
            var getterILCode = getterMethod.GetILGenerator();
            getterILCode.Emit(OpCodes.Ldarg_0);
            getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
            getterILCode.Emit(OpCodes.Ret);

            // Define the setter method.
            var setterMethod = builder.DefineMethod(
                string.Concat(setterPrefix, propertyName),
                propertyMethodAttributes, null, new[] { propertyType });

            // Emit the IL code.
            // ldarg.0
            // ldarg.1
            // stfld,_field
            // ret
            var setterILCode = setterMethod.GetILGenerator();
            setterILCode.Emit(OpCodes.Ldarg_0);
            setterILCode.Emit(OpCodes.Ldarg_1);
            setterILCode.Emit(OpCodes.Stfld, fieldBuilder);
            setterILCode.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getterMethod);
            propertyBuilder.SetSetMethod(setterMethod);
        }
    }
}