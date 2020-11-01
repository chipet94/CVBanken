import EducationService from "@/services/EducationService";

const initialState = {
    programmes: [],
    userProgramme: {},
    categories: [],
    students: []
}

export const edu = {
    namespaced: true,
    state: initialState,
    getters: {
        getAllLocal: state => {
            return state.programmes
        },
        studentsInProgramme: (state) => (id) => {
            return state.students.filter(p => p.programmeId === id)
        },
        getCategories: state => {
            return state.categories
        },
        getCategory: state => (id) => {
            return state.categories.find(p => p.id === id)
        },
        getCategoryFromName: state => (name) => {
            return state.categories.find(category => category.name.toUpperCase() === name.toUpperCase())
        },
        getProgrammeFromName: state => name => {
            return state.programmes.find(p => p.name === name)
        }

    },
    actions: {

        getAllCategories({commit}) {
            return EducationService.getCategories().then(
                categories => {
                    commit('categoriesSuccess', categories);
                    return Promise.resolve(categories);
                },
                error => {
                    return Promise.reject(error);
                }
            )
        },
        getCategoryByName({commit}, name) {
            return EducationService.getCategoryByName(name).then(
                category => {
                    commit('categorySuccess', category);
                    return Promise.resolve(category);
                },
                error => {
                    return Promise.reject(error);
                }
            )
        },
        getCategoryById({commit}, id) {
            return EducationService.getCategoryById(id).then(
                categories => {
                    commit('categorySuccess', categories);
                    return Promise.resolve(categories);
                },
                error => {
                    return Promise.reject(error);
                }
            )
        },
        getAll({commit}) {
            return EducationService.getEducations().then(
                educations => {
                    commit('educationsSuccess', educations);
                    return Promise.resolve(educations);
                },
                error => {
                    return Promise.reject(error);
                }
            )
        },
        getById({commit}, id) {
            return EducationService.getEducation(id).then(
                education => {
                    commit("educationSuccess", education)
                    return Promise.resolve(education);
                },
                error => {
                    return Promise.reject(error);
                }
            )
        },
        getByName({commit}, name) {
            return EducationService.getEducationByName(name).then(
                education => {
                    commit("educationSuccess", education)
                    return Promise.resolve(education);
                },
                error => {
                    return Promise.reject(error);
                }
            )
        },
        getStudentsIn({commit}, id) {
            return EducationService.getStudentsInProgramme(id).then(
                students => {
                    commit("studentsSuccess", students)
                    return Promise.resolve(students);
                },
                error => {
                    return Promise.reject(error);
                }
            )

        },
        getByCategoryId({commit}, id) {
            return EducationService.getEducationByCategory(id).then(
                education => {
                    commit("educationSuccess", education)
                    return Promise.resolve(education);
                },
                error => {
                    return Promise.reject(error);
                }
            )

        }
    },
    mutations: {
        educationsSuccess(state, educations) {
            state.programmes = educations;
        },
        categoriesSuccess(state, categories) {
            state.categories = categories;
        },
        categorySuccess(state, category) {
            console.log(category)
            let target = state.categories.find(cat => {return category.id === cat.id;});

            target ? Object.assign(target, category) : state.categories.push(category);
            console.log(state.categories)
        },
        studentsSuccess(state, students) {
            students.forEach(sourceElement => {
                let targetElement = state.students.find(targetElement => {
                    return sourceElement['id'] === targetElement['id'];
                })
                targetElement ? Object.assign(targetElement, sourceElement) : state.students.push(sourceElement);
            })
        }
        ,
        educationSuccess(state, education) {
            let target = state.programmes.find(edu => {return edu.id === education.id;});

            target ? Object.assign(target, education) : state.programmes.push(education);
        },
        registerSuccess(state) {
            state.status.loggedIn = false;
        },
        educationsFailure(state) {
            state.status.loggedIn = false;
        }
    }
}
