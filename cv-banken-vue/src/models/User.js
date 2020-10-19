import UserFileService from "@/services/UserFileService";
import EducationService from "@/services/EducationService";
export default class User {
    email;
    firstName;
    lastName;
    role;
    programmeId;
    token;
    id;
    profile;
    files = [];
    programme = {};
    constructor(data = {}) {
        this.firstName = data.firstName;
        this.lastName = data.lastName;
        this.email = data.email;
        this.role = data.role;
        this.programmeId = data.programmeId;
        this.class_name = data.class_name;
        this.categoryName = data.categoryName;
        this.token = data.token;
    }
    async getProgramme(){
        return await EducationService.getEducation(this.programmeId).then(res => {
            this.programme =  res;
            console.log(res)
        });
    }

    async getFiles(){
        return await UserFileService.getAllByUser(this.id).then(res => {
            this.files = res;
        })
    }

    static FromData(data) {
        if (data != null) {
            return new User(data)
        }
        return User.EmptyUser();
    }

    static EmptyUser(){
        return new User()
    }

}
