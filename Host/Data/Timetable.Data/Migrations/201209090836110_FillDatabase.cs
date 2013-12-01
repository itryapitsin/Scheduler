namespace Timetable.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FillDatabase : DbMigration
    {
        public override void Up()
        {
            String query = String.Empty;

            #region Add faculties

            query = @"
                SET IDENTITY_INSERT [dbo].[Faculties] ON
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (1, N'����������� ���������', N'�����������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (2, N'������������� ���������', N'�������������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (3, N'������������ ���������', N'������������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (4, N'��������� ������������ � ���������� ����', N'������������ � ���������� ����')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (5, N'��������� ������������-������� ��������� � ��������', N'������������-������� ��������� � ��������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (6, N'��������������� ���������', N'���������������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (7, N'������-����������� ���������', N'������-�����������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (8, N'�������-������������� ���������', N'�������-�������������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (9, N'������������ ���������', N'������������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (10, N'�������������� ���������', N'��������������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (11, N'�������������� ���������', N'��������������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (12, N'������� �������', N'�������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (13, N'����������� ���������', N'�����������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (14, N'�����-������������� ���������', N'�����-�������������')
                INSERT [dbo].[Faculties] ([Id], [Name], [ShortName]) VALUES (15, N'�������������� ���������', N'��������������')
                SET IDENTITY_INSERT [dbo].[Faculties] OFF";

            Sql(query);

            #endregion

            #region Add departments

            query = @"
                SET IDENTITY_INSERT [dbo].[Departments] ON
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (1, N'������� ���������� �������� � ����������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (2, N'������� ��������������� � �������������� ��������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (3, N'������� ���������� �������� � ��������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (4, N'������� ����������� ������ ������-������������� ��������������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (5, N'������� ������������ ���������� ��������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (6, N'������� ������������ � ���', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (7, N'������� ������������� �������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (8, N'������� ������������ ��������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (9, N'������� ����������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (10, N'������� ����������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (11, N'������� ���������� � �����������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (12, N'������� ����� � ������������� ��������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (13, N'������� ������������ �������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (14, N'������� ���������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (15, N'������� ����������� � ������������� ��������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (16, N'������� ������� ��������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (17, N'������� ������� ����������� � ������� �������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (18, N'������� �������� �������� (����� ��������� ��������)', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (19, N'������� ������������ �������� � ������ �������������', NULL, 1)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (20, N'������� �������������� �����, ������� ������������� ������������ � ������', NULL, 2)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (21, N'������� ������������� ��������� � ���������������� � �������������� ����������', NULL, 2)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (22, N'������� ������������� ������ � ��������', NULL, 2)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (23, N'������� �����������', NULL, 2)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (24, N'������� ��������� � ���������� �������������', NULL, 2)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (25, N'������� �����������, ������������ ����������� � ����������', NULL, 3)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (26, N'������� ����������� ������������� ������������', NULL, 3)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (27, N'������� �������������� ��������� � ���������� �������', NULL, 3)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (28, N'������� ��������', NULL, 3)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (29, N'������� �������������, ���������� � �������������', NULL, 3)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (30, N'������� ������ ������������������� ��������������', NULL, 3)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (31, N'������� �����������', NULL, 4)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (32, N'������� ����������', NULL, 4)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (33, N'������� ���������� ������', NULL, 4)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (34, N'������� ������������� ���������', NULL, 4)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (35, N'������� ����������� ������ ���������� ������������ � ���������� ����', NULL, 4)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (36, N'������� �������� ����� � ����������', NULL, 5)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (37, N'������� ����������� � ��������� ������', NULL, 5)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (38, N'������� ���������, ��������������� � ���������', NULL, 6)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (39, N'������� ���������, ������������� � ���������� ����������������� �������', NULL, 6)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (40, N'������� ����������� ��������������������� ������������', NULL, 6)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (41, N'������� ����� ������', NULL, 7)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (42, N'������� ������ �������� ����', NULL, 7)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (43, N'������� �������������-������������� ������ � ���������� �����������', NULL, 7)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (44, N'������� ����������� � �����������������', NULL, 7)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (45, N'������� ����������������� ����������� � ����������������', NULL, 7)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (46, N'������� �������� �����', NULL, 15)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (47, N'������� ������� ���������� � ������������', NULL, 15)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (48, N'������� ���������� ���������', NULL, 15)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (49, N'������� ������������ ���������', NULL, 15)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (50, N'������� ������� ������������� ������', NULL, 15)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (51, N'������� ����� �����', NULL, 8)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (52, N'������� �������� � ���������� ��������', NULL, 8)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (53, N'������� �������� � ��������', NULL, 8)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (54, N'������� ������������ ��������, ������������� � ������������ �����', NULL, 8)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (55, N'������� �������� � ���������', NULL, 14)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (56, N'������� ������� ����', NULL, 14)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (57, N'������� �������� �������', NULL, 9)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (58, N'������� ������� ��������������� ������', NULL, 9)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (59, N'������� ������������� ������� (������������������ ������)', NULL, 9)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (60, N'������� ������� ����� �������� ������', NULL, 9)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (61, N'������� ������������� � ����������� ������������ ���������', NULL, 9)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (62, N'������� ������� �����', NULL, 10)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (63, N'������� ���������� � ������������ ������� ���������', NULL, 10)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (64, N'������� ������������� ���������� � ��������', NULL, 10)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (65, N'������� ������� ���������', NULL, 10)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (66, N'������� ���������� �������� � �������', NULL, 10)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (67, N'������� ����������-�������� � �������������������� �����������', NULL, 10)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (68, N'������� ������ ������������ � ������� ������', NULL, 11)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (69, N'������� ����������� � ��������������� �����������', N'���', 11)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (70, N'������� ��������������� �������', NULL, 11)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (71, N'������� ���������� ���������� � �����������', N'����', 11)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (72, N'������� ��������������� ������������� ������ ����������', NULL, 11)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (73, N'������� ��������� � ���������', NULL, 11)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (74, N'������� ������, ������� ����������� � �����', NULL, 13)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (75, N'������� �������������� � ���������������� �����', NULL, 13)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (76, N'������� ����������-�������� ���������', NULL, 13)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (77, N'������� ��������-�������� ���������', NULL, 13)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (78, N'������� ����������� ������ ������������ �����������', NULL, NULL)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (79, N'������� ����������� ������ ����������� �����������', NULL, NULL)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (80, N'������� �������������', NULL, NULL)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (81, N'���� ������������ �����������������', NULL, NULL)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (82, N'������� ���������� � ����������', NULL, NULL)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (83, N'������� ����������� ���������� � ������', NULL, NULL)
                INSERT [dbo].[Departments] ([Id], [Name], [ShortName], [Faculty_Id]) VALUES (84, N'������� ���������', NULL, NULL)
                SET IDENTITY_INSERT [dbo].[Departments] OFF";
            Sql(query);
            #endregion

            #region Add specialities

            query = @"
                SET IDENTITY_INSERT [dbo].[Specialities] ON
                INSERT [dbo].[Specialities] ([Id], [Name], [ShortName], [Code], [Department_Id]) VALUES (1, N'�������������� ������� � ����������', N'����', N'230201.65', 69)
                INSERT [dbo].[Specialities] ([Id], [Name], [ShortName], [Code], [Department_Id]) VALUES (2, N'�������������� ������� � ����������', N'����', N'230400.62', 69)
                INSERT [dbo].[Specialities] ([Id], [Name], [ShortName], [Code], [Department_Id]) VALUES (3, N'�������������� ������� � ����������', N'����', N'230400.68', 69)
                INSERT [dbo].[Specialities] ([Id], [Name], [ShortName], [Code], [Department_Id]) VALUES (4, N'������-�����������', N'��', N'080500.62', 71)
                INSERT [dbo].[Specialities] ([Id], [Name], [ShortName], [Code], [Department_Id]) VALUES (5, N'������-�����������', N'��', N'080500.68', 71)
                INSERT [dbo].[Specialities] ([Id], [Name], [ShortName], [Code], [Department_Id]) VALUES (6, N'����������', NULL, N'010100.62', 70)
                INSERT [dbo].[Specialities] ([Id], [Name], [ShortName], [Code], [Department_Id]) VALUES (7, N'����������', NULL, N'010100.68', 70)
                INSERT [dbo].[Specialities] ([Id], [Name], [ShortName], [Code], [Department_Id]) VALUES (8, N'����������', NULL, N'010101.65', 70)
                INSERT [dbo].[Specialities] ([Id], [Name], [ShortName], [Code], [Department_Id]) VALUES (9, N'���������� ���������� � �����������', N'���', N'010400.62', 71)
                INSERT [dbo].[Specialities] ([Id], [Name], [ShortName], [Code], [Department_Id]) VALUES (10, N'���������� ���������� � �����������', N'���', N'010400.68', 71)
                INSERT [dbo].[Specialities] ([Id], [Name], [ShortName], [Code], [Department_Id]) VALUES (11, N'���������� ���������� � �����������', N'���', N'010501.65', 71)
                SET IDENTITY_INSERT [dbo].[Specialities] OFF";

            Sql(query);

            #endregion

            #region Add courses

            query = @"
                SET IDENTITY_INSERT [dbo].[Courses] ON
                INSERT [dbo].[Courses] ([Id], [Name]) VALUES (1, N'������ ����')
                INSERT [dbo].[Courses] ([Id], [Name]) VALUES (2, N'������ ����')
                INSERT [dbo].[Courses] ([Id], [Name]) VALUES (3, N'������ ����')
                INSERT [dbo].[Courses] ([Id], [Name]) VALUES (4, N'��������� ����')
                INSERT [dbo].[Courses] ([Id], [Name]) VALUES (5, N'����� ����')
                INSERT [dbo].[Courses] ([Id], [Name]) VALUES (6, N'������ ����')
                SET IDENTITY_INSERT [dbo].[Courses] OFF";
            Sql(query);
            #endregion

            #region Add groups

            query = @"
                SET IDENTITY_INSERT [dbo].[Groups] ON
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (1, 1, N'22101', 8)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (2, 1, N'22103', 11)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (3, 1, N'22104', 11)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (4, 1, N'22105', 3)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (5, 1, N'22106', 3)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (6, 1, N'22108', 5)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (7, 2, N'22201', 8)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (8, 2, N'22203', 11)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (9, 2, N'22204', 11)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (10, 2, N'22205', 3)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (11, 2, N'22206', 3)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (12, 2, N'22208', 5)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (13, 3, N'22301', 8)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (14, 3, N'22303', 11)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (15, 3, N'22304', 11)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (16, 3, N'22305', 3)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (17, 3, N'22306', 3)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (18, 3, N'22308', 5)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (19, 4, N'22401', 8)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (20, 4, N'22403', 11)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (21, 4, N'22404', 11)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (22, 4, N'22405', 3)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (23, 4, N'22406', 3)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (24, 4, N'22408', 5)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (25, 5, N'22501', 8)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (26, 5, N'22503', 11)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (27, 5, N'22504', 11)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (28, 5, N'22505', 3)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (29, 5, N'22506', 3)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (30, 5, N'22507', 5)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (31, 5, N'22509', 5)
                INSERT [dbo].[Groups] ([Id], [Course_Id], [Code], [Speciality_Id]) VALUES (32, 5, N'22510', 8)
                SET IDENTITY_INSERT [dbo].[Groups] OFF";

            Sql(query);

            #endregion

            #region Add ranks

            query = @"
                SET IDENTITY_INSERT [dbo].[Ranks] ON
                INSERT [dbo].[Ranks] ([Id], [Name], [Position]) VALUES (1, N'���������� ��������', 1)
                INSERT [dbo].[Ranks] ([Id], [Name], [Position]) VALUES (2, N'�.�. ���. ��������', 2)
                INSERT [dbo].[Ranks] ([Id], [Name], [Position]) VALUES (3, N'���������', 3)
                INSERT [dbo].[Ranks] ([Id], [Name], [Position]) VALUES (4, N'������', 4)
                INSERT [dbo].[Ranks] ([Id], [Name], [Position]) VALUES (5, N'������� �������������', 5)
                INSERT [dbo].[Ranks] ([Id], [Name], [Position]) VALUES (6, N'�������������', 6)
                INSERT [dbo].[Ranks] ([Id], [Name], [Position]) VALUES (7, N'�������� �������', 7)
                INSERT [dbo].[Ranks] ([Id], [Name], [Position]) VALUES (8, N'�������', 8)
                SET IDENTITY_INSERT [dbo].[Ranks] OFF";

            Sql(query);

            #endregion

            #region Add lecturers

            query = @"
                SET IDENTITY_INSERT [dbo].[Lecturers] ON
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (1,	N'�������',		N'��������',	N'����������',		N'voronin@psu.karelia.ru ', 71, 1)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (2,	N'��������',	N'�������',		N'������������',	N'schegoleva@psu.karelia.ru ', 71, 2)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (3,	N'�����',		N'����',		N'����������',		N'zaika@krc.karelia.ru ', 71, 3)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (4,	N'��������',	N'��������',	N'����������',		N'kuznetcv@mail.ru ', 71, 3)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (5,	N'�������',		N'�����',		N'����������',		N'emorozov@karelia.ru ', 71, 3)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (6,	N'�������',		N'�����',		N'������������',	N'rvoronov@karelia.ru ', 71, 4)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (7,	N'�������',		N'�������',		N'����������',		N'lazarev_av@sampo.ru ', 71, 4)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (8,	N'���������',	N'�����',		N'�������',			N'onasad@psu.karelia.ru ', 71, 4)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (9,	N'���������',	N'������',		N'����������',		N'persn@newmail.ru ', 71, 4)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (10,	N'�������',		N'�����',		N'����������',		N'iaminova@karelia.ru ', 71, 4)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (11,	N'������',		N'�����',		N'��������',		N'ashabaev@karelia.ru ', 71, 4)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (12,	N'�����',		N'�����',		N'������������',	N'zhukov@karelia.ru ', 71, 5)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (13,	N'���������',	N'���������',	N'����������',		N'kiritigr@rambler.ru ', 71, 5)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (14,	N'�������',		N'�������',		N'��������',		N'kositsyn@psu.karelia.ru ', 71, 5)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (15,	N'������',		N'�����',		N'������������',	N'soshkin@mail.ru ', 71, 5)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (16,	N'���������',	N'�������',		N'����������',		N'tsurovceva@psu.karelia.ru ', 71, 5)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (17,	N'�����',		N'���������',	N'����������',		N'sysun@petrsu.ru ', 71, 5)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (18,	N'�����',		N'�������',		N'�������',			N'alex5431@mail.ru ', 71, 5)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (19,	N'��������',	N'����',		N'�������������',	N'julana2008@yandex.ru ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (20,	N'��������',	N'���������',	N'����������',		N'anastasiyamih@mail.ru ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (21,	N'��������',	N'������',		N'����������',		NULL, 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (22,	N'���������',	N'���������',	N'������������',	N'bogdanov@karelia.ru ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (23,	N'������',		N'�����',		N'��������',		N'dpetrovich@onego.ru ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (24,	N'��������',	N'����',		N'����������',		N'voronova_am@petrsu.ru ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (25,	N'��������',	N'�����',		N'����������',		N'antonvoropaev@mail.ru ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (26,	N'��������',	N'�������',		N'������������',	N'natgn@goon.ru ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (27,	N'�������',		N'�������',		N'�������������',	N'gorinov@karelia.ru ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (28,	N'��������',	N'�������',		N'���������',		NULL, 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (29,	N'�����',		N'����',		N'����������',		N'http://gusev.eleset.ru', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (30,	N'������',		N'�������',		N'����������',		NULL, 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (31,	N'��������',	N'�����',		N'����������',		N'http://karavaev.flowproblem.ru', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (32,	N'���������',	N'����',		N'����������',		NULL, 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (33,	N'�������',		N'������',		N'����������',		N'mcsymco@gmail.com ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (34,	N'��������',	N'�������',		N'����������',		N'nola@psu.karelia.ru ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (35,	N'�������',		N'��������',	N'�������������',	N'mail@gyrr.ru ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (36,	N'������',		N'����',		N'���������',		N'Julia.Sidorova@metsopartners.com ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (37,	N'�������',		N'���������',	N'����������',		N'svl@karelia.ru ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (38,	N'������',		N'�����',		N'�����������',		N'foelan@petrsu.ru ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (39,	N'������',		N'������',		N'������������',	N'sergey.khentov@gmail.com ', 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (40,	N'����',		N'������',		N'����������',		NULL, 71, 6)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (41,	N'��������',	N'�������',		N'���������',		N'pmik@petrsu.ru ', 71, 7)
                INSERT [dbo].[Lecturers] ([Id], [Lastname], [Firstname], [Middlename],  [Contacts], [Department_Id], [Rank_Id]) VALUES (42,	N'��������',	N'������',		N'���������',		NULL, 71, 8)
                SET IDENTITY_INSERT [dbo].[Lecturers] OFF";

            Sql(query);

            #endregion

            #region Add tutorials

            query = @"
                SET IDENTITY_INSERT [dbo].[Tutorials] ON
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (1, N'���������� ����������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (2, N'���. ������� ���. � �����.', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (3, N'����. ���������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (4, N'��������� �� ����������������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (5, N'����������������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (6, N'����. ������ �����������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (7, N'����. ���������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (8, N'������������� ���������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (9, N'�� ������ � ����������������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (10, N'������ ����������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (11, N'������ �����������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (12, N'����������� � ���. ������ �������� �������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (13, N'���������� ������ �����������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (14, N'�/� �������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (15, N'�/� ����� ���������� ��� ��������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (16, N'�/� ���������� ���������� ���������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (17, N'������ ������������ � ��', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (18, N'�������������� ��', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (19, N'������������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (20, N'������. �����. � Oracle', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (21, N'������������� ��', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (22, N'������������ ��������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (23, N'������������ ���������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (24, N'���������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (25, N'���. �����������. ���. ������������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (26, N'�/� ����. ���������� ������������. ����������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (27, N'�/� ����. ���������. ����. ���������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (28, N'�/� ������ ������� � �����', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (29, N'������ ����������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (30, N'���������� �������������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (31, N'�/� ������ ���������� ����������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (32, N'�/� ��������� ������ ����. ���.', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (33, N'��������� ��������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (34, N'�����������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (35, N'������ �������� �������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (36, N'���������� �� ��', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (37, N'���������� ���. �������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (38, N'�/� �������������� ����� �������������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (39, N'�/� ����������� ������� ����������', NULL)
                INSERT [dbo].[Tutorials] ([Id], [Name], [ShortName]) VALUES (40, N'���������� �������������', NULL)
                SET IDENTITY_INSERT [dbo].[Tutorials] OFF";
            Sql(query);
            #endregion

            #region Add buildings

            query = @"
                SET IDENTITY_INSERT [dbo].[Buildings] ON
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (1, N'������� ������', N'��. ������ 33', N'��', NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (2, N'������� ������ �1', N'��. ������� 20', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (3, N'������� ������ �2', N'���. �������� 17', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (4, N'������� ������ �3', N'��. ���������� �������� 8', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (5, N'������� ������ �4', N'��. ���������� �������� 48', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (6, N'������-������������ ������ �5', N'��. ��������������� 10', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (7, N'������-������������ ������ �6', N'��. ��������������� 10�', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (8, N'������� ������ �7', N'���. �������� 3', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (9, N'������� ������ �8', N'��. ���������� 65', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (10, N'������� ������ �9', N'��. ������ 31', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (11, N'������������� ������', N'��. ��������������� 31', N'��', NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (12, N'��������������� ������', N'��. ������ 31', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (13, N'�������', N'��. ��������������� 31�', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (14, N'��������� �1', N'��. ���������� 63', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (15, N'��������� �3', N'��. ��������������� 31�', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (16, N'��������� �4', N'��. ����������� 15', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (17, N'��������� �5', N'��. ������ 11�', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (18, N'��������� �6', N'��. ����������� 17', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (19, N'��������� �7', N'��. ������ 7�', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (20, N'��������� �8', N'��. ���������� 5', NULL, NULL)
                INSERT [dbo].[Buildings] ([Id], [Name], [Address], [ShortName], [Info]) VALUES (21, N'����� � ������������ ����� ���', N'��. �������� 15�', NULL, NULL)
                SET IDENTITY_INSERT [dbo].[Buildings] OFF";
            Sql(query);

            #endregion

            #region Add auditoriums
            query = @"
                SET IDENTITY_INSERT [dbo].[Auditoriums] ON
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (1, N'1', NULL, 12, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (2, N'2', NULL, 12, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (3, N'3', NULL, 12, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (4, N'4', NULL, 12, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (5, N'5', NULL, 12, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (6, N'6', NULL, 12, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (7, N'7', NULL, 12, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (8, N'8', NULL, 12, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (9, N'9', NULL, 12, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (10, N'10', NULL, 12, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (11, N'11', NULL, 12, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (12, N'12', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (13, N'13', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (14, N'14', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (15, N'15', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (16, N'16', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (17, N'17', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (18, N'18', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (19, N'19', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (20, N'20', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (21, N'21', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (22, N'22', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (23, N'23', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (24, N'24', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (25, N'25', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (26, N'26', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (27, N'27', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (28, N'28', NULL, 30, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (29, N'29', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (30, N'30', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (31, N'31', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (32, N'32', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (33, N'33', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (34, N'34', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (35, N'35', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (36, N'36', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (37, N'37', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (38, N'38', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (39, N'39', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (40, N'40', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (41, N'41', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (42, N'42', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (43, N'43', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (44, N'44', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (45, N'45', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (46, N'46', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (47, N'47', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (48, N'48', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (49, N'49', NULL, 60, NULL, 1)
                INSERT [dbo].[Auditoriums] ([Id], [Number], [Name], [Capacity], [Info], [Building_Id]) VALUES (50, N'50', NULL, 100, NULL, 1)
                SET IDENTITY_INSERT [dbo].[Auditoriums] OFF";

            Sql(query);
            #endregion

            #region Add times

            query = @"
                SET IDENTITY_INSERT [dbo].[Times] ON
                INSERT [dbo].[Times] ([Id], [Start], [End]) VALUES (1, '8:00', '9:35')
                INSERT [dbo].[Times] ([Id], [Start], [End]) VALUES (2, '9:45', '11:20')
                INSERT [dbo].[Times] ([Id], [Start], [End]) VALUES (3, '11:30', '13:05')
                INSERT [dbo].[Times] ([Id], [Start], [End]) VALUES (4, '13:30', '15:05')
                INSERT [dbo].[Times] ([Id], [Start], [End]) VALUES (5, '15:15', '16:50')
                INSERT [dbo].[Times] ([Id], [Start], [End]) VALUES (6, '17:00', '18:35')
                INSERT [dbo].[Times] ([Id], [Start], [End]) VALUES (7, '18:45', '20:20')
                INSERT [dbo].[Times] ([Id], [Start], [End]) VALUES (8, '20:30', '22:05')
                SET IDENTITY_INSERT [dbo].[Times] OFF";

            Sql(query);

            #endregion

            #region Add week types

            query = @"
                SET IDENTITY_INSERT [dbo].[TutorialTypes] ON
                INSERT [dbo].[TutorialTypes] ([Id], [Name]) VALUES (1, N'������')
                INSERT [dbo].[TutorialTypes] ([Id], [Name]) VALUES (2, N'������������ �������')
                INSERT [dbo].[TutorialTypes] ([Id], [Name]) VALUES (3, N'������������ ������')
                SET IDENTITY_INSERT [dbo].[TutorialTypes] OFF";
            Sql(query);
            #endregion
        }
        
        public override void Down()
        {
            Sql("Delete from Auditoriums");
            Sql("Delete from Buildings");
            Sql("Delete from Courses");
            Sql("Delete from Departments");
            Sql("Delete from Faculties");
            Sql("Delete from Ranks");
            Sql("Delete from Groups");
            Sql("Delete from Lecturers");
            Sql("Delete from Specialities");
            Sql("Delete from Times");
            Sql("Delete from Tutorials");
            Sql("Delete from TutorialTypes");
            Sql("Delete from WeekTypes");
        }
    }
}
