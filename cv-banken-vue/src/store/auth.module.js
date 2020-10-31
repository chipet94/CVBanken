import AuthService from '../services/AuthService';
import {SessionData} from "@/models/SessionData";
import User from "@/models/User";

let session = JSON.parse(localStorage.getItem("sessionData"));
const initialState = session
    ? {status: {loggedIn: true}, session}
    : {status: {loggedIn: false}, session: null};

export const auth = {
    namespaced: true,
    state: initialState,
    getters: {
        getSession: state => {
            return SessionData.FromData(state.session)
        },
        getUser: state => {
            return User.FromData(state.session);
        }
    },
    actions: {
        login({commit}, request) {
            return AuthService.login(request).then(
                user => {
                    commit('loginSuccess', user);
                    return Promise.resolve(user);
                },
                error => {
                    commit('loginFailure');
                    console.log(error.toString())
                    return Promise.reject(error);
                }
            );
        },
        logout({commit}) {
            AuthService.logout();
            commit('logout');
        },
        register({commit}, user) {
            return AuthService.register(user).then(
                response => {
                    commit('registerSuccess');
                    return Promise.resolve(response.data);
                },
                error => {
                    commit('registerFailure');
                    return Promise.reject(error);
                }
            );
        },
    },
    mutations: {
        loginSuccess(state, user) {
            state.status.loggedIn = true;
            state.session = user;
        },
        loginFailure(state) {
            state.status.loggedIn = false;
            state.session = null;
        },
        logout(state) {
            state.status.loggedIn = false;
            state.session = null;
        },
        registerSuccess(state) {
            state.status.loggedIn = false;
        },
        registerFailure(state) {
            state.status.loggedIn = false;
        }
    }
};
