
var currentTabId = 1;

function SettingsController($scope, $controller) {
    $controller('BaseController', { $scope: $scope });

    $scope.refreshDataClick = function() {
    };
}

function UserSettingsController($scope, $http, $rootScope, $modal, $controller) {
    $scope.login = pageModel.login;
    $scope.firstname = pageModel.firstname;
    $scope.middlename = pageModel.middlename;
    $scope.lastname = pageModel.lastname;
    $controller('BaseController', { $scope: $scope });
    
    $scope.save = function () {
        var params = {
            firstname: $scope.firstname,
            middlename: $scope.middlename,
            lastname: $scope.lastname,
        };

        $http
            .post($http.prefix + 'Settings/Save',  params)
            .success(function (response) {
                if (response.ok) {
                    
                    $scope.alert = { content: 'Данные сохранены', ok: $scope.hideDialog };
                    $scope.showDialog('alert.html');
                    
                    $rootScope.$broadcast('settingsUpdated', response.userName);

                    return;
                } 

                if (response.message)
                    $scope.message = response.message;
                else
                    $scope.message = 'Ошибка запроса';
            });
    };
}

function AuditoriumsController($scope, $modal, $http) {
    $scope.buildings;
    $scope.auditoriums;
    $scope.currentBuildingId;

    if ($scope.currentBuildingId) {
        $scope.currentBuildingId = parseInt($scope.currentBuildingId);
    }
    $scope.buildingChanged = function () {
        console.log("changeBuilding");
        loadAuditoriums();
    };

    $scope.loadBuildings = function () {
        console.log("loadBuildings");
        $http
           .get($http.prefix + 'Settings/GetBuildings', {})
           .success(function (response) {
               console.log(response);
               $scope.buildings = response.buildings;
               $scope.currentBuildingId = response.currentBuildingId;
               if (response.currentBuildingId !== undefined)
                   loadAuditoriums();
           });
    }

    function loadAuditoriums() {
        console.log("loadAuditoriums");
        $http
           .get($http.prefix + 'Settings/GetAuditoriums', { params: { buildingId: $scope.currentBuildingId, pageNumber: 1, pageSize: 10 } })
           .success(function (response) {
               console.log(response);
               $scope.auditoriums= response;
           });
    }

  

    $scope.delete = function (auditorium) {

        var params = {
            auditoriumId: auditorium.id
        };

        $scope.confirm = {
            content: "Вы действительно хотите удалить аудиторию №'{0}' ?".replace('{0}', auditorium.number),
            ok: function () {
                $http
                    .post($http.prefix + 'Settings/DeleteAuditorium', params)
                    .success(function (response) {
                        for (var i in $scope.auditoriums) {
                            if ($scope.auditoriums[i].id == auditorium.id) {
                                $scope.auditoriums.splice(i, 1);
                                break;
                            }
                        }
                    });
                $scope.hideDialog();
            },
            cancel: function () {
                $scope.hideDialog();
            }
        };
        $scope.showDialog('confirmation.html');
    };


    $scope.$on('auditoriumCreated', function (e, auditorium) {
        $scope.auditoriums.push(auditorium);
    });

    $scope.$on('auditoriumUpdated', function (e, auditorium) {
        for (var i in $scope.auditoriums) {
            if ($scope.auditoriums[i].id == auditorium.id) {
                $scope.auditoriums[i] = auditorium;
                break;
            }
        }
    });

    $scope.edit = function (auditorium) {

        console.log(auditorium);
        $scope.auditoriumId = auditorium.id;
        $scope.number = auditorium.number;
        $scope.name = auditorium.name;
        $scope.info = auditorium.info;
        $scope.capacity = auditorium.capacity;
        $scope.currentBuildingIdAuditoriumModal = auditorium.buildingId;
        $scope.currentAuditoriumTypeIdAuditoriumModal = auditorium.auditoriumTypeId;
        $scope.isEdit = true;
        $scope.showDialog('editauditoriummodal.html');
    };

    
    $scope.showCreateAuditoriumDialog = function () {
        $scope.showDialog('editauditoriummodal.html');
    };
}

function CreateEditAuditoriumModalController($scope, $http, $controller, $rootScope) {
    $controller('BaseController', { $scope: $scope });

    
    $scope.buildingsAuditoriumModal;
    $scope.auditoriumTypesAuditoriumModal;

   

    $scope.loadModalData = function () {
        console.log("loadModalData");

        console.log($scope.auditoriumId);

        //if ($scope.auditoriumId === undefined) {
            //$scope.currentBuildingIdAuditoriumModal = undefined;
            //$scope.currentAuditoriumTypeIdAuditoriumModal = undefined;
        //}

        $http
           .get($http.prefix + 'Settings/GetAuditoriumCreateEditModalData', {})
           .success(function (response) {
               console.log(response);
               $scope.buildingsAuditoriumModal = response.buildings;
               $scope.auditoriumTypesAuditoriumModal = response.auditoriumTypes;
           });
    }

    $scope.ok = function () {
        var params = {
            number: $scope.number,
            name: $scope.name,
            info: $scope.info,
            capacity: $scope.capacity,
            buildingId: $scope.currentBuildingIdAuditoriumModal,
            auditoriumTypeId: $scope.currentAuditoriumTypeIdAuditoriumModal,
            auditoriumId: $scope.auditoriumId
        };

        $http
            .post($http.prefix + 'Settings/CreateEditAuditorium', params)
            .success(function (response) {
                if (response.ok) {
                    $scope.hideDialog();
                    if ($scope.auditoriumId !== undefined) {
                        $rootScope.$broadcast('auditoriumUpdated', {
                            id: $scope.auditoriumId,
                            number: $scope.number,
                            name: $scope.name,
                            info: $scope.info,
                            capacity: $scope.capacity,
                            buildingId: $scope.currentBuildingIdAuditoriumModal,
                            auditoriumTypeId: $scope.currentAuditoriumTypeIdAuditoriumModal
                        });
                    } else {
                        $rootScope.$broadcast('auditoriumCreated', {
                            number: $scope.number,
                            name: $scope.name,
                            info: $scope.info,
                            capacity: $scope.capacity,
                            buildingId: $scope.currentBuildingIdAuditoriumModal,
                            auditoriumTypeId: $scope.currentAuditoriumTypeIdAuditoriumModal
                        });
                    }
                }

                if (response.message)
                    $scope.message = response.message;
                else
                    $scope.message = 'Ошибка запроса';
            });
    };

    $scope.cancel = function () {
        this.hideDialog();
    };
}



function AuditoriumTypesController($scope, $modal, $http) {
    $scope.auditoriumTypes;

    $scope.loadAuditoriumTypes = function(){
        console.log("loadAuditoriumTypes");
        $http
           .get($http.prefix + 'Settings/GetAuditoriumTypes', {})
           .success(function (response) {
               $scope.auditoriumTypes = response;
           });
    }

    $scope.delete = function (auditoriumType) {
        var params = {
            auditoriumTypeId: auditoriumType.id
        };

        $scope.confirm = {
            content: "Вы действительно хотите удалить тип аудитории: '{0}' ?".replace('{0}', auditoriumType.name),
            ok: function () {
                $http
                    .post($http.prefix + 'Settings/DeleteAuditoriumType', params)
                    .success(function (response) {
                        for (var i in $scope.auditoriumTypes) {
                            if ($scope.auditoriumTypes[i].id == auditoriumType.id) {
                                $scope.auditoriumTypes.splice(i, 1);
                                break;
                            }
                        }
                    });
                $scope.hideDialog();
            },
            cancel: function () {
                $scope.hideDialog();
            }
        };
        $scope.showDialog('confirmation.html');
    };

    $scope.$on('auditoriumTypeCreated', function (e, auditoriumType) {
        $scope.auditoriumTypes.push(auditoriumType);
    });

    $scope.$on('auditoriumTypeUpdated', function (e, auditoriumType) {
        for (var i in $scope.auditoriumTypes) {
            if ($scope.auditoriumTypes[i].id == auditoriumType.id) {
                $scope.auditoriumTypes[i] = auditoriumType;
                break;
            }
        }
    });

    $scope.edit = function (auditoriumType) {

        $scope.auditoriumTypeId = auditoriumType.id;
        $scope.name = auditoriumType.name;
        $scope.pattern = auditoriumType.pattern;
        $scope.isEdit = true;
        $scope.showDialog('editauditoriumtypemodal.html');
    };

    $scope.showCreateAuditoriumTypeDialog = function () {
        $scope.showDialog('editauditoriumtypemodal.html');
    };
}


function CreateEditAuditoriumTypeModalController($scope, $http, $controller, $rootScope) {
    $controller('BaseController', { $scope: $scope });

    $scope.ok = function () {
        var params = {
            name: $scope.name,
            pattern: $scope.pattern,
            auditoriumTypeId: $scope.auditoriumTypeId
        };

        $http
            .post($http.prefix + 'Settings/CreateEditAuditoriumType', params)
            .success(function (response) {
                if (response.ok) {
                    $scope.hideDialog();

                    if ($scope.auditoriumTypeId !== undefined) {
                        $rootScope.$broadcast('auditoriumTypeUpdated', {
                            id: $scope.auditoriumTypeId,
                            name: $scope.name,
                            pattern: $scope.pattern
                        });
                    } else {
                        $rootScope.$broadcast('auditoriumTypeCreated', {
                            name: $scope.name,
                            pattern: $scope.pattern
                        });
                    }
                }

                if (response.message)
                    $scope.message = response.message;
                else
                    $scope.message = 'Ошибка запроса';
            });
    };

    $scope.cancel = function () {
        this.hideDialog();
    };
}


function WeekTypesController($scope, $modal, $http) {
    $scope.weekTypes;

    $scope.loadWeekTypes = function () {
        console.log("loadWeekTypes");
        $http
           .get($http.prefix + 'Settings/GetWeekTypes', {})
           .success(function (response) {
               $scope.weekTypes = response;
           });
    }

    $scope.delete = function (weekType) {
        var params = {
            weekTypeId: weekType.id
        };

        $scope.confirm = {
            content: "Вы действительно хотите удалить тип недели: '{0}' ?".replace('{0}', weekType.name),
            ok: function () {
                $http
                    .post($http.prefix + 'Settings/DeleteWeekType', params)
                    .success(function (response) {
                        for (var i in $scope.weekTypes) {
                            if ($scope.weekTypes[i].id == weekType.id) {
                                $scope.weekTypes.splice(i, 1);
                                break;
                            }
                        }
                    });
                $scope.hideDialog();
            },
            cancel: function () {
                $scope.hideDialog();
            }
        };
        $scope.showDialog('confirmation.html');
    };

    $scope.$on('weekTypeCreated', function (e, weekType) {
        $scope.weekTypes.push(weekType);
    });

    $scope.$on('weekTypeUpdated', function (e, weekType) {
        for (var i in $scope.weekTypes) {
            if ($scope.weekTypes[i].id == weekType.id) {
                $scope.weekTypes[i] = weekType;
                break;
            }
        }
    });

    $scope.edit = function (weekType) {

        $scope.weekTypeId = weekType.id;
        $scope.name = weekType.name;
        $scope.isEdit = true;
        $scope.showDialog('editweektypemodal.html');
    };

    $scope.showCreateWeekTypeDialog = function () {
        $scope.showDialog('editweektypemodal.html');
    };
}


function CreateEditWeekTypeModalController($scope, $http, $controller, $rootScope) {
    $controller('BaseController', { $scope: $scope });

    $scope.ok = function () {
        var params = {
            name: $scope.name,
            weekTypeId: $scope.weekTypeId
        };

        $http
            .post($http.prefix + 'Settings/CreateEditWeekType', params)
            .success(function (response) {
                if (response.ok) {
                    $scope.hideDialog();
                    if ($scope.weekTypeId !== undefined) {
                        $rootScope.$broadcast('weekTypeUpdated', {
                            id: $scope.weekTypeId,
                            name: $scope.name
                        });
                    } else {
                        $rootScope.$broadcast('weekTypeCreated', {
                            name: $scope.name
                        });
                    }
                }

                if (response.message)
                    $scope.message = response.message;
                else
                    $scope.message = 'Ошибка запроса';
            });
    };

    $scope.cancel = function () {
        this.hideDialog();
    };
}

function TutorialTypesController($scope, $modal, $http) {
    $scope.tutorialTypes;

    $scope.loadTutorialTypes = function () {
        console.log("loadTutorialTypes");
        $http
           .get($http.prefix + 'Settings/GetTutorialTypes', {})
           .success(function (response) {
               $scope.tutorialTypes = response;
           });
    }

    $scope.delete = function (tutorialType) {
        var params = {
            tutorialTypeId: tutorialType.id
        };

        $scope.confirm = {
            content: "Вы действительно хотите удалить тип предмета: '{0}' ?".replace('{0}', tutorialType.name),
            ok: function () {
                $http
                    .post($http.prefix + 'Settings/DeleteTutorialType', params)
                    .success(function (response) {
                        for (var i in $scope.tutorialTypes) {
                            if ($scope.tutorialTypes[i].id == tutorialType.id) {
                                $scope.tutorialTypes.splice(i, 1);
                                break;
                            }
                        }
                    });
                $scope.hideDialog();
            },
            cancel: function () {
                $scope.hideDialog();
            }
        };
        $scope.showDialog('confirmation.html');
    };

    $scope.$on('tutorialTypeCreated', function (e, tutorialType) {
        $scope.tutorialTypes.push(tutorialType);
    });

    $scope.$on('tutorialTypeUpdated', function (e, tutorialType) {
        for (var i in $scope.tutorialTypes) {
            if ($scope.tutorialTypes[i].id == tutorialType.id) {
                $scope.tutorialTypes[i] = tutorialType;
                break;
            }
        }
    });

    $scope.edit = function (tutorialType) {

        $scope.tutorialTypeId = tutorialType.id;
        $scope.name = tutorialType.name;
        $scope.isEdit = true;
        $scope.showDialog('edittutorialtypemodal.html');
    };

    $scope.showCreateTutorialTypeDialog = function () {
        $scope.showDialog('edittutorialtypemodal.html');
    };
}


function CreateEditTutorialTypeModalController($scope, $http, $controller, $rootScope) {
    $controller('BaseController', { $scope: $scope });

    $scope.ok = function () {
        var params = {
            name: $scope.name,
            tutorialTypeId: $scope.tutorialTypeId
        };

        $http
            .post($http.prefix + 'Settings/CreateEditTutorialType', params)
            .success(function (response) {
                if (response.ok) {
                    $scope.hideDialog();
                    if ($scope.tutorialTypeId !== undefined) {
                        $rootScope.$broadcast('tutorialTypeUpdated', {
                            id: $scope.tutorialTypeId,
                            name: $scope.name
                        });
                    } else {
                        $rootScope.$broadcast('tutorialTypeCreated', {
                            name: $scope.name
                        });
                    }
                }

                if (response.message)
                    $scope.message = response.message;
                else
                    $scope.message = 'Ошибка запроса';
            });
    };

    $scope.cancel = function () {
        this.hideDialog();
    };
}

function ScheduleTypesController($scope, $modal, $http) {
    $scope.scheduleTypes;

    $scope.loadScheduleTypes = function () {
        console.log("loadScheduleTypes");
        $http
           .get($http.prefix + 'Settings/GetScheduleTypes', {})
           .success(function (response) {
               $scope.scheduleTypes = response;
           });
    }

    $scope.delete = function (scheduleType) {
        var params = {
            scheduleTypeId: scheduleType.id
        };

        $scope.confirm = {
            content: "Вы действительно хотите удалить тип расписания: '{0}' ?".replace('{0}', scheduleType.name),
            ok: function () {
                $http
                    .post($http.prefix + 'Settings/DeleteScheduleType', params)
                    .success(function (response) {
                        for (var i in $scope.scheduleTypes) {
                            if ($scope.scheduleTypes[i].id == scheduleType.id) {
                                $scope.scheduleTypes.splice(i, 1);
                                break;
                            }
                        }
                    });
                $scope.hideDialog();
            },
            cancel: function () {
                $scope.hideDialog();
            }
        };
        $scope.showDialog('confirmation.html');
    };

    $scope.$on('scheduleTypeCreated', function (e, scheduleType) {
        $scope.scheduleTypes.push(scheduleType);
    });

    $scope.$on('scheduleTypeUpdated', function (e, scheduleType) {
        for (var i in $scope.scheduleTypes) {
            if ($scope.scheduleTypes[i].id == scheduleType.id) {
                $scope.scheduleTypes[i] = scheduleType;
                break;
            }
        }
    });

    $scope.edit = function (scheduleType) {

        $scope.scheduleTypeId = scheduleType.id;
        $scope.name = scheduleType.name;
        $scope.isEdit = true;
        $scope.showDialog('editscheduletypemodal.html');
    };

    $scope.showCreateScheduleTypeDialog = function () {
        $scope.showDialog('editscheduletypemodal.html');
    };
}


function CreateEditScheduleTypeModalController($scope, $http, $controller, $rootScope) {
    $controller('BaseController', { $scope: $scope });

    $scope.ok = function () {
        var params = {
            name: $scope.name,
            scheduleTypeId: $scope.scheduleTypeId
        };

        $http
            .post($http.prefix + 'Settings/CreateEditScheduleType', params)
            .success(function (response) {
                if (response.ok) {
                    $scope.hideDialog();
                    if ($scope.scheduleTypeId !== undefined) {
                        $rootScope.$broadcast('scheduleTypeUpdated', {
                            id: $scope.scheduleTypeId,
                            name: $scope.name
                        });
                    } else {
                        $rootScope.$broadcast('scheduleTypeCreated', {
                            name: $scope.name
                        });
                    }
                }

                if (response.message)
                    $scope.message = response.message;
                else
                    $scope.message = 'Ошибка запроса';
            });
    };

    $scope.cancel = function () {
        this.hideDialog();
    };
}

function StudyTypesController($scope, $modal, $http) {
    $scope.studyTypes;

    $scope.loadStudyTypes = function () {
        console.log("loadStudyTypes");
        $http
           .get($http.prefix + 'Settings/GetStudyTypes', {})
           .success(function (response) {
               $scope.studyTypes = response;
           });
    }

    $scope.delete = function (studyType) {
        var params = {
            studyTypeId: studyType.id
        };

        $scope.confirm = {
            content: "Вы действительно хотите удалить тип обучения: '{0}' ?".replace('{0}', studyType.name),
            ok: function () {
                $http
                    .post($http.prefix + 'Settings/DeleteStudyType', params)
                    .success(function (response) {
                        for (var i in $scope.studyTypes) {
                            if ($scope.studyTypes[i].id == studyType.id) {
                                $scope.studyTypes.splice(i, 1);
                                break;
                            }
                        }
                    });
                $scope.hideDialog();
            },
            cancel: function () {
                $scope.hideDialog();
            }
        };
        $scope.showDialog('confirmation.html');
    };

    $scope.$on('studyTypeCreated', function (e, studyType) {
        $scope.studyTypes.push(studyType);
    });

    $scope.$on('studyTypeUpdated', function (e, studyType) {
        for (var i in $scope.studyTypes) {
            if ($scope.studyTypes[i].id == studyType.id) {
                $scope.studyTypes[i] = studyType;
                break;
            }
        }
    });

    $scope.edit = function (studyType) {

        $scope.studyTypeId = studyType.id;
        $scope.name = studyType.name;
        $scope.isEdit = true;
        $scope.showDialog('editstudytypemodal.html');
    };

    $scope.showCreateScheduleTypeDialog = function () {
        $scope.showDialog('editstudytypemodal.html');
    };
}


function CreateEditStudyTypeModalController($scope, $http, $controller, $rootScope) {
    $controller('BaseController', { $scope: $scope });

    $scope.ok = function () {
        var params = {
            name: $scope.name,
            studyTypeId: $scope.studyTypeId
        };

        $http
            .post($http.prefix + 'Settings/CreateEditStudyType', params)
            .success(function (response) {
                if (response.ok) {
                    $scope.hideDialog();
                    if ($scope.studyTypeId !== undefined) {
                        $rootScope.$broadcast('studyTypeUpdated', {
                            id: $scope.studyTypeId,
                            name: $scope.name
                        });
                    } else {
                        $rootScope.$broadcast('studyTypeCreated', {
                            name: $scope.name
                        });
                    }
                }

                if (response.message)
                    $scope.message = response.message;
                else
                    $scope.message = 'Ошибка запроса';
            });
    };

    $scope.cancel = function () {
        this.hideDialog();
    };
}

function SemestersController($scope, $modal, $http) {
    $scope.semesters;

    $scope.loadSemesters = function () {
        console.log("loadSemesters");
        $http
           .get($http.prefix + 'Settings/GetSemesters', {})
           .success(function (response) {
               $scope.semesters = response;
           });
    }

    $scope.delete = function (semester) {
        var params = {
            semesterId: semester.id
        };

        $scope.confirm = {
            content: "Вы действительно хотите удалить семестр: '{0}' ?".replace('{0}', semester.name),
            ok: function () {
                $http
                    .post($http.prefix + 'Settings/DeleteSemester', params)
                    .success(function (response) {
                        for (var i in $scope.semesters) {
                            if ($scope.semesters[i].id == semester.id) {
                                $scope.semesters.splice(i, 1);
                                break;
                            }
                        }
                    });
                $scope.hideDialog();
            },
            cancel: function () {
                $scope.hideDialog();
            }
        };
        $scope.showDialog('confirmation.html');
    };

    $scope.$on('semesterCreated', function (e, semester) {
        $scope.semesters.push(semester);
    });

    $scope.$on('semesterUpdated', function (e, semester) {
        for (var i in $scope.semesters) {
            if ($scope.semesters[i].id == semester.id) {
                $scope.semesters[i] = semester;
                break;
            }
        }
    });

    $scope.edit = function (semester) {

        $scope.semesterId = semester.id;
        $scope.name = semester.name;
        $scope.isEdit = true;
        $scope.showDialog('editsemestermodal.html');
    };

    $scope.showCreateSemesterDialog = function () {
        $scope.showDialog('editsemestermodal.html');
    };
}


function CreateEditSemesterModalController($scope, $http, $controller, $rootScope) {
    $controller('BaseController', { $scope: $scope });

    $scope.ok = function () {
        var params = {
            name: $scope.name,
            semesterId: $scope.semesterId
        };

        $http
            .post($http.prefix + 'Settings/CreateEditSemester', params)
            .success(function (response) {
                if (response.ok) {
                    $scope.hideDialog();
                    if ($scope.semesterId !== undefined) {
                        $rootScope.$broadcast('semesterUpdated', {
                            id: $scope.semesterId,
                            name: $scope.name
                        });
                    } else {
                        $rootScope.$broadcast('semesterCreated', {
                            name: $scope.name
                        });
                    }
                }

                if (response.message)
                    $scope.message = response.message;
                else
                    $scope.message = 'Ошибка запроса';
            });
    };

    $scope.cancel = function () {
        this.hideDialog();
    };
}

function StudyYearsController($scope, $modal, $http) {
    $scope.studyYears;

    $scope.loadStudyYears = function () {
        console.log("loadStudyYears ");
        $http
           .get($http.prefix + 'Settings/GetStudyYears', {})
           .success(function (response) {
               $scope.studyYears = response;
           });
    }

    $scope.delete = function (studyYear) {
        var params = {
            studyYearId: studyYear.id
        };

        $scope.confirm = {
            content: "Вы действительно хотите удалить год обучения: '{0}' ?".replace('{0}', studyYear.name),
            ok: function () {
                $http
                    .post($http.prefix + 'Settings/DeleteStudyYear', params)
                    .success(function (response) {
                        for (var i in $scope.studyYears) {
                            if ($scope.studyYears[i].id == studyYear.id) {
                                $scope.studyYears.splice(i, 1);
                                break;
                            }
                        }
                    });
                $scope.hideDialog();
            },
            cancel: function () {
                $scope.hideDialog();
            }
        };
        $scope.showDialog('confirmation.html');
    };

    $scope.$on('studyYearCreated', function (e, studyYear) {
        $scope.studyYears.push(studyYear);
    });

    $scope.$on('studyYearUpdated', function (e, studyYear) {
   
        for (var i in $scope.studyYears) {
      
            if ($scope.studyYears[i].id == studyYear.id) {
                $scope.studyYears[i] = studyYear;
                break;
            }
        }
    });

    $scope.edit = function (studyYear) {

        var splits = studyYear.name.split("/");
        var startYear = parseInt(splits[0]);
        var endYear = parseInt(splits[1]);
        var length = endYear-startYear;

        $scope.studyYearId = studyYear.id;
        $scope.startYear = startYear;
        $scope.length = length;
        $scope.isEdit = true;
        $scope.showDialog('editstudyyearmodal.html');
    };

    $scope.showCreateStudyYearDialog = function () {
        $scope.showDialog('editstudyyearmodal.html');
    };
}


function CreateEditStudyYearModalController($scope, $http, $controller, $rootScope) {
    $controller('BaseController', { $scope: $scope });

    $scope.ok = function () {
        var params = {
            startYear: $scope.startYear,
            length: $scope.length,
            studyYearId: $scope.studyYearId  
        };

        $http
            .post($http.prefix + 'Settings/CreateEditStudyYear', params)
            .success(function (response) {
                if (response.ok) {
                    $scope.hideDialog();
                 

                    if ($scope.studyYearId !== undefined) {
                        $rootScope.$broadcast('studyYearUpdated', {
                            id: $scope.studyYearId,
                            name: $scope.startYear + "/" + (parseInt($scope.startYear) + parseInt($scope.length)).toString()
                        });
                    } else {
                        $rootScope.$broadcast('studyYearCreated', {
                            name: $scope.startYear + "/" + (parseInt($scope.startYear) + parseInt($scope.length)).toString()
                        });
                    }
                }

                if (response.message)
                    $scope.message = response.message;
                else
                    $scope.message = 'Ошибка запроса';
            });
    };

    $scope.cancel = function () {
        this.hideDialog();
    };
}

function UsersController($scope, $modal, $http) {
    $scope.users = pageModel.users;

    $scope.$on('userCreated', function(e, user) {
        $scope.users.push(user);
    });

    $scope.delete = function (user) {
        $scope.confirm = {
            content: "Вы действительно хотите удалить пользователя '{0}' ?".replace('{0}', user.login),
            ok: function () {
                $http
                    .post()
                    .success(function(response) {

                    });

                $scope.hideDialog();
            },
            cancel: function() {
                $scope.hideDialog();
            }
        };
        $scope.showDialog('confirmation.html');
    };
    
    $scope.edit = function (user) {
        $scope.login = user.login;
        $scope.firstname = user.firstname;
        $scope.middlename = user.middlename;
        $scope.lastname = user.lastname;
        $scope.isEdit = true;

        $scope.showDialog('editusermodal.html');
    };
    
    $scope.showCreateUserDialog = function () {
        $scope.showDialog('editusermodal.html');
    };
}

function CreateEditUserModalController($scope, $http, $controller, $rootScope) {
    $controller('BaseController', { $scope: $scope });

    $scope.ok = function () {
        var params = {
            firstname: $scope.firstname,
            middlename: $scope.middlename,
            lastname: $scope.lastname,
            login: $scope.login,
            password: $scope.password,
            confirmPassword: $scope.confirmPassword
        };

        $http
            .post($http.prefix + 'Settings/CreateEditUser', params)
            .success(function(response) {
                if (response.ok) {
                    $scope.hideDialog();
                    $rootScope.$broadcast('userCreated', {
                        firstname: $scope.firstname,
                        middlename: $scope.middlename,
                        lastname: $scope.lastname,
                        login: $scope.login,
                    });
                }

                if (response.message)
                    $scope.message = response.message;
                else
                    $scope.message = 'Ошибка запроса';
            });
    };

    $scope.cancel = function () {
        this.hideDialog();
    };
}

function DataSyncController($scope) {
    var hub = $.connection.dispatcherHub;

    $scope.logs = [];
    $scope.moment = moment;
    $scope.isSyncStarted = false;

    $scope.syncDataClick = function () {
        $scope.logs = [];
        $scope.isSyncStarted = true;
        hub.server.refreshData();
    };
    
    hub.client.refreshLog = function (message, isCompleted) {
        $scope.$apply(function () {
            $scope.logs.push("[" + moment(new Date()).format("DD.MM.YYYY hh:mm") + "]: " + message);
            $scope.isSyncStarted = !isCompleted;
        });
    };
    
    $.connection.hub.start();
}