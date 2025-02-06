import axios from 'axios';
axios.defaults.baseURL = "http://localhost:5282";
// axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;
axios.defaults.headers.post['Content-Type'] = 'application/json';

axios.defaults.baseURL = "http://localhost:5282";

axios.interceptors.response.use(
  (response) => response, 
  (error) => {
    console.error("Axios Error:", error.response?.status, error.response?.data);
    return Promise.reject(error);
  }
);

export default {
  getTasks: async () => {
    const result = await axios.get(`/items`)    
    return result.data;
  },

  addTask: async(name)=>{
    console.log('addTask', name)
    const result=await axios.post(`/items`, {name})
    return result.data;
  },

  setCompleted: async(id, isComplete)=>{
    console.log('setCompleted', {id, isComplete})
    const result=await axios.put(`/items/${id}`, {isComplete})
    console.log(result.data)
    return result.data;
  },

  deleteTask:async(id)=>{
    console.log('deleteTask')
   const result= await axios.delete(`/items/${id}`)
    return result.data;
  },
};
