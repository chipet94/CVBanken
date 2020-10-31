import UserFileService from "@/services/UserFileService";
import EducationService from "@/services/EducationService";
import ProfileService from "@/services/profileService";
export default class User {
    email;
    firstName;
    lastName;
    programmeId;
    id;
    profile;
    files = [];
    programme = {};
    url;
    private;
    searching;
    class_name;
    description;
    profilePicture;
    gotCv;
    constructor(data = {}) {
        this.id = data.id,
        this.firstName = data.firstName;
        this.lastName = data.lastName;
        this.email = data.email;
        this.programmeId = data.programmeId;
        this.class_name = data.class_name;
        this.categoryName = data.categoryName;
        this.url = data.url;
        this.searching = data.searching;
        this.private = data.private;
        this.gotCv = data.gotCv;
        this.description = data.description;
        this.profilePictureId = data.profilePictureId ?? '';
        this.profilePicture = data.profilePicture;
    }
    async getProgramme(){
        return await EducationService.getEducation(this.programmeId).then(res => {
            this.programme =  res;
            console.log(res)
        });
    }
    async getProfilePicture() {
        await ProfileService.getProfilePicture(this.id).then(res => {
            console.log(res)
            if(res.status === 200){
                let reader = new FileReader();
                //reader.readAsDataURL(res.data)
                reader.readAsDataURL(res.data);
                reader.onload = () => {
                    this.profilePicture = reader.result;
                }
            }
            // this.profilePicture =  'data:image/jpg;base64,'.concat(this.profilePicture.concat(res.data));

        }).catch(err => {
            console.log(err)
            this.profilePicture = null
        })
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
