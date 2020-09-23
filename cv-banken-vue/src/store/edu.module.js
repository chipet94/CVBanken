import EducationService from "@/services/EducationService";

const initialState = {
    programmes: [],
    userProgramme: {}
}

export const edu = {
    namespaced: true,
    state: initialState,
    getters:{
        getAllLocal : state => {
            return state.programmes
        },
    },
    actions:{
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
                    commit("educationSuccess")
                    return Promise.resolve(education);
                }
            )

        }
    },
    mutations: {
        educationsSuccess(state, educations) {
            state.programmes = educations;
        },
        loginFailure(state) {
            state.status.loggedIn = false;
            state.user = null;
        },
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
