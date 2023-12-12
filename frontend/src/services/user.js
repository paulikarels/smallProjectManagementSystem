import axios from 'axios'
import { variables } from './variables.js'
const baseUrl = variables.USER_API


const getAll = async () => {
  const request = await axios.get(baseUrl)

  return request.data
}

const create = async (newObject) => {
  try {
    const response = await axios.post(baseUrl, newObject)
    return response.data
  } catch (error) {
    console.log(error.response.data)
    return false
  }
}


export default { getAll, create }