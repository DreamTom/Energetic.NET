import Http from '../http';

export const getResourceTree = function(){
  return Http.get('/resources')
}