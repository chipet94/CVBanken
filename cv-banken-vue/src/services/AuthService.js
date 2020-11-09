import axios from "axios";
import {authHeader} from "@/services/AuthHeader";

const API_URL = process.env.VUE_APP_API_URL

class AuthService {
    login(user) {
        return axios
            .post(API_URL + 'auth/authenticate', {
                email: user.email,
                password: user.password
            })
            .then(response => {
                if (response.data.token) {
                    // console.log(response.data)
                    localStorage.setItem('sessionData', JSON.stringify(response.data));
                }
                return response.data;
            });
    }

    logout() {
        localStorage.removeItem('sessionData');
    }

    register(user) {
        return axios.post(API_URL + 'auth/register', {
            email: user.email,
            password: user.password,
            firstName: user.firstName,
            lastName: user.lastName,
            programmeId: user.programmeId,

        });
    }
    loggedIn(){
        return axios.get(API_URL + "auth/loggedin",
            {
                headers: authHeader(),
            }).then(response => {
            if (response.data.token) {
                localStorage.setItem('sessionData', JSON.stringify(response.data));
            }
            return response.data;
        })
    }
}

export default new AuthService();
