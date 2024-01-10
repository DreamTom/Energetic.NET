import Http from '../http';

export const uploadImageUrl = Http.config.baseURL + '/files/uploadImage'

export const getFileUrl = function(id : string){
  return Http.config.baseURL + '/files/'+ id;
}


export const delFile = function(id: string){
  return Http.delete('/files/' + id)
}