using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Reflection;
using System.Reflection.Emit;
using Timetable.Data.Mapping;
using Timetable.Data.Models;
using Timetable.Data.Models.Personalization;
using Timetable.Data.Models.Scheduler;


namespace Timetable.Data.Context
{
    public class SchedulerContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TutorialType> TutorialTypes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<ScheduleInfo> ScheduleInfoes { get; set; }
        public DbSet<WeekType> WeekTypes { get; set; }
        public DbSet<AuditoriumType> AuditoriumTypes { get; set; }
        public DbSet<StudyYear> StudyYears { get; set; }
        public DbSet<ScheduleType> ScheduleTypes { get; set; }
        public DbSet<StudyType> StudyTypes { get; set; }
        public DbSet<Semester> Semesters { get; set; }

        public SchedulerContext()
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true;
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.ValidateOnSaveEnabled = false;
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new AuditoriumMapping());
            modelBuilder.Configurations.Add(new AuditoriumTypeMapping());
            modelBuilder.Configurations.Add(new BranchMapping());
            modelBuilder.Configurations.Add(new BuildingMapping());
            modelBuilder.Configurations.Add(new CourseMapping());
            modelBuilder.Configurations.Add(new DepartmentMapping());
            modelBuilder.Configurations.Add(new FacultyMapping());
            modelBuilder.Configurations.Add(new GroupMapping());
            modelBuilder.Configurations.Add(new ScheduleMapping());
            modelBuilder.Configurations.Add(new SpecialityMapping());
            modelBuilder.Configurations.Add(new TimeMapping());
            modelBuilder.Configurations.Add(new TutorialMapping());
            modelBuilder.Configurations.Add(new ScheduleInfoMapping());
            modelBuilder.Configurations.Add(new LecturersMapping());
            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new StudyTypeMapping());
        }

        public virtual void Add<TEntity>(TEntity entity, bool isApplyNow = true) where TEntity : class
        {
            AttachIfNotAttached(entity);
            Set(entity.GetType()).Add(entity);
            if (isApplyNow)
                SaveChanges();
        }

        public virtual void Update<TEntity>(TEntity entity, bool isApplyNow = true) where TEntity : class
        {
            AttachIfNotAttached(entity);
            Entry(entity).State = EntityState.Modified;
            if (isApplyNow)
                SaveChanges();
        }

        public virtual void Delete<TEntity>(TEntity entity, bool isApplyNow = true) where TEntity : class
        {
            AttachIfNotAttached(entity);
            Set(entity.GetType()).Remove(entity);
            Entry(entity).State = EntityState.Deleted;
            if (isApplyNow)
                SaveChanges();
        }

        public void AttachIfNotAttached<TEntity>(TEntity entity) where TEntity : class 
        {
            if (Entry(entity).State != EntityState.Detached)
                return;
            Set(entity.GetType()).Attach(entity);
        }

        public IQueryable<TEntity> RawSqlQuery<TEntity>(
            string query,
            params object[] parameters) where TEntity : class
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
