import { useState, useEffect } from 'react'
import userService from '../services/user'
import projectService from '../services/projects'
import loginService from '../services/login'

const loginForm = ({ failureMessage, setUser}) => {
  const [username, setUsername] = useState('SwaggerTest')
  const [password, setPassword] = useState('SwaggerTest')
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

      setPassword('')
    } catch (exception) {
      console.log('wrong username or password')
      failureMessage('wrong username or password')
    }
  }
  
  const handleRegister = async (event) => {
    event.preventDefault()

    const userObject = {
      Username: username,
      Password: password
    }
    await userService.create(userObject)

    setUsername('')
    setPassword('')
    setIsRegistering(false)
  }




  return (
    <div className="container">
      <div className="form-box">
        <h1>{isRegistering ? 'Register' : 'Login'} to Application</h1>
        <form onSubmit={isRegistering ? handleRegister : handleLogin}>
          <div>
            <label>Username:</label>
            <input
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </div>
          <div>
            <label>Password:</label>
            <input
              type="password"
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