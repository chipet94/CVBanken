import ProfileService from "@/services/profileService";
import User from "@/models/User";


export class Profile {
    profileId;
    url;
    private;
    searching;
    description;
    userId;
    profilePictureId;
    profilePicture;

    constructor(data = {}) {
        this.profileId = data.profileId
        this.url = data.url;
        this.searching = data.searching;
        this.private = data.private;
        this.description = data.description;
        this.userId = data.userId;
        this.profilePictureId = data.profilePictureId ?? '';
        this.profilePicture = data.profilePicture
    }

    async getUser() {
        return new User( await ProfileService.getUserById(this.userId))
    }

    async getProfilePicture() {
            await ProfileService.getProfilePicture(this.profileId).then(res => {
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
}
