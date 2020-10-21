import UserService from "@/services/UserService";
const initialState = {
    publicUsers: [],
    allUsers: []
}

export const user = {
    namespaced: true,
    state: initialState,
    getters:{
        admin_GetAllUsers: state => {
            return state.allUsers;
        },
        AllPublicUsers: state => {
            return state.publicUsers;
        }
    },
    actions:{
        all({commit}){
            return UserService.getAll().then(
                users => {
                    commit("usersSuccess", users.data)
                    return Promise.resolve(users.data)
                },
                err => {
                    console.log(err);
                    return Promise.reject(err)
                }
            )
        },
        admin_all({commit}){
            return UserService.adminGetAll().then(
                users => {
                    commit("adminUsersSuccess", users.data)
                    return Promise.resolve(users.data)
                },
                err => {
                    console.log(err);
                    return Promise.reject(err)
                }
            )
        },
    },
    mutations: {
        adminUsersSuccess(state, users) {
            state.allUsers = users
        },
        usersSuccess(state, users) {
            state.publicUsers = users
        },


    }
}