import axios from "axios";

const API_URL = process.env.VUE_APP_API_URL
class SiteService {
    getHome() {
        return axios
            .get(API_URL + 'site/')
            .then(response => {
                return response.data;
            });
    }
}

export default new SiteService();