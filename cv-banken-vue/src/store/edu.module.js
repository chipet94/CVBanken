import EducationService from "@/services/EducationService";

const initialState = {
    programmes: [],
    userProgramme: {},
    categories:[],
    students: []
}

export const edu = {
    namespaced: true,
    state: initialState,
    getters:{
        getAllLocal : state => {
            return state.programmes
        },
        studentsInProgramme : (state) => (id) => {
            return state.students.filter(p => p.programmeId === id)
        },
        getCategories : state => {
            return state.categories
        },

    },
    actions:{

        getAllCategories({commit}){
            return EducationService.getEducationCategories().then(
                categories => {
                    commit('categoriesSuccess', categories);
                    return Promise.resolve(categories);
                },
                error => {
                    return Promise.reject(error);
                }

            )
        },
        getAll({commit}){
            return EducationService.getEducations().then(
                educations => {
                    commit('educationsSuccess', educations);
                    return Promise.resolve(educations);
                },
                error => {
                    commit('educationsFailure');
                    return Promise.reject(error);
                }

            )
        },
        getById({commit}, id){
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
        getStudentsIn({commit}, id){
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
        getByCategoryId({ commit }, id) {
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
        studentsSuccess(state, students) {
                students.forEach(sourceElement => {
                    let targetElement = state.students.find(targetElement => {
                        return sourceElement['id'] === targetElement['id'];
                    })
                    targetElement ? Object.assign(targetElement, sourceElement) : state.students.push(sourceElement);
                })
        }
        ,
        educationSuccess(state, edu) {
            state.programmes.map(prog => prog.id === edu.id
                ?{...prog, edu}
                : prog
            );
        },
        registerSuccess(state) {
            state.status.loggedIn = false;
        },
        educationsFailure(state) {
            state.status.loggedIn = false;
        }
    }
}
