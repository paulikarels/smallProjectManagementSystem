import axios from 'axios'
import { variables } from './variables.js'
const baseUrl = variables.USER_API


const getAll = async () => {
  const request = await axios.get(baseUrl)

  return request.data
}

const create = async (newObject) => {

  const response = await axios.post(baseUrl, newObject)
  return response.data
}


export default { getAll, create }