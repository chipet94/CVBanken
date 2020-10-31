export const updateProfileModel = request => {
    return {
        firstName: request.firstName,
        lastName: request.lastName,
        programmeId: request.programmeId,
        email: request.email,
        oldPassword: request.oldPassword,
        password: request.password,
        private: request.private,
        searching: request.searching,
        description: request.description,
    }

}
