import { useState, useEffect } from 'react'
import userService from '../services/user'
import projectService from '../services/projects'
import loginService from '../services/login'

const loginForm = ({ successMessage, failureMessage, setUser}) => {
  const [username, setUsername] = useState('User')
  const [password, setPassword] = useState('User')
  //  const [password, setPassword] = useState('SwaggerTest')
  const [isRegistering, setIsRegistering] = useState(false)

  useEffect(() => {
    const loggedUserJSON = window.localStorage.getItem('loggedNoteappUser')
    if (loggedUserJSON) {
      const user = JSON.parse(loggedUserJSON)
      setUser(user)
      
      projectService.setToken(user.token)
    }
  }, [])

  const handleLogin = async (event) => {
    event.preventDefault()
    
    try {
      const user = await loginService.login({
        username, password,
      })
      window.localStorage.setItem(
        'loggedNoteappUser', JSON.stringify(user)
      )
      projectService.setToken(user.token)
      setUser(user)
      successMessage(`Login successful`)
      setPassword('')
    } catch (exception) {
      failureMessage('wrong username or password')
      console.log('wrong username or password')
      //failureMessage('wrong username or password')
    }
  }
  
  const handleRegister = async (event) => {
    event.preventDefault()
    try {
      const userObject = {
        Username: username,
        Password: password
      }
      const user = await userService.create(userObject)

      if (user === false){
        failureMessage('User already exists or incorrect credentials')
        return
      } 
      setUsername('')
      setPassword('')
      setIsRegistering(false)
    } catch (ex) {
      console.log(ex)
    }
  }




  return (
    <div className="container">
      <div className="form-box">
        <h1>{isRegistering ? 'Register' : 'Login'} to Application</h1>
        <form onSubmit={isRegistering ? handleRegister : handleLogin}>
          <div>
            <label>Username:</label>
            <input
              type="text" required minLength="3" 
              maxLength="35"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </div>
          <div>
            <label>Password:</label>
            <input
              type="password" required minLength="3" 
              maxLength="35"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>
          <button type="submit">{isRegistering ? 'Register' : 'Login'}</button>
          <button type="button" onClick={() => setIsRegistering(!isRegistering)}>
            {isRegistering ? 'Go to Login' : 'Go to Register'}
          </button>
        </form>
      </div>
    </div>
  )
}

export default  loginForm