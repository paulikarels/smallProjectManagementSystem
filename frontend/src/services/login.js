import axios from 'axios'
import { variables } from './variables.js'
const baseUrl = variables.LOGIN_API

const login = async credentials => {
  const response = await axios.post(baseUrl, credentials)
  return response.data
}

export default { login }