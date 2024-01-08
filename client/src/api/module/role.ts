import Http from '../http';

export const addRole = function(addForm: any){
  return Http.post('/roles', addForm);
}

export const editRole = function(editForm: any){
  return Http.put(`/roles/${editForm.id}`, editForm);
}

export const delRole = function(id: string){
  return Http.delete('/roles/'+id);
}

export const getRoles = function(queryForm: any){
  return Http.get('/roles', queryForm);
}

