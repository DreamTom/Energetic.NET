import Http from '../http';

export const getResourceTree = function(queryForm: any){
  return Http.get('/resources', queryForm)
}

export const getMenuTree = function(){
  return Http.get('/resources/menuTree')
}

export const addResource = function(addForm:any){
  return Http.post('/resources', addForm)
}

export const editResource = function(editForm:any){
  return Http.put(`/resources/${editForm.id}`, editForm)
}

export const delResource = function(id: string){
  return Http.delete('/resources/'+id)
}