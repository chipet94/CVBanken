import axios from "axios";
import {authHeader} from "@/services/AuthHeader";

const API_URL = process.env.VUE_APP_API_URL
class AuthService {
    login(user) {
        return axios
            .post(API_URL + 'user/authenticate', {
                email: user.email,
                password: user.password
            })
            .then(response => {
                if (response.data.token) {
                   // console.log(response.data)
                    localStorage.setItem('userData', JSON.stringify(response.data));
                }
                return response.data;
            });
    }

    logout() {
        localStorage.removeItem('userData');
    }

    register(user) {
        return axios.post(API_URL + 'user/register', {
            email: user.email,
            password: user.password,
            firstName: user.firstName,
            lastName: user.lastName,
            programmeId: user.programmeId,

        });
    }
    getProfile(url = ""){
        return axios.get(API_URL + "profile/" + url, {
        })
    }
    getCurrentUserProfile(){
        return axios.get(API_URL + "profile/", {
            headers: authHeader(),
            credentials: 'include'
        })
    }
    getProfilePicture(url = ""){
        return axios.get(API_URL + "profile/" + url + "/picture")
    }
}

export default new AuthService();
