export const UploadFileRequest = request => {
    return {
        files: request.files,
        isCv: request.isCv ?? false,
        owner: request.owner ?? "",
    }
}

