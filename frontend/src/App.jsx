import { useState, useRef } from 'react'
import LoginForm from './components/LoginForm'
import Togglable from './components/Togglable'
import ProjectForm from './components/Project/ProjectForms'
import Projects from './components/Project/Projects'
import Notification from './components/Notification'

import './index.css'
function App() {
  const [projects, setProjects] = useState([])
  const [tasks, setTasks] = useState([])
  const [user, setUser] = useState(null)
  const [ message, setErrorMessage] = useState({ message: null, style: "success"})

  const successMessage = (value) => {
    setErrorMessage(
      { message: value, style: 'success' }
    )
    setTimeout(() => {
      setErrorMessage({ message: null })
    }, 3000)
  }

  const failureMessage = (value) => {
    setErrorMessage(
      { message: value, style: 'error' }
    )
    setTimeout(() => {
      setErrorMessage({ message: null })
    }, 3000)
  }
  const projectFormRef = useRef()

  const projectForm = () => (
    <Togglable buttonLabel='new project' ref={projectFormRef}>
      <ProjectForm  setProjects={setProjects} user={user} successMessage={successMessage} failureMessage={failureMessage}/>
    </Togglable>
  )

  const handleLogout = async (event) => {
    event.preventDefault()
    window.localStorage.removeItem('loggedNoteappUser')
    window.location.reload()
  }

  return (
    <>
      <div className="app-container">
        <div className="user-info">
          {user ? (
            <>
              <Notification message={message.message} className={message.style}/>
              <h2>{user.user.username} logged in</h2>
              {projectForm()}
              <button onClick={handleLogout}>Logout</button>
            </>
          ) : (
            <div> 
              <Notification message={message.message} className={message.style}/>
              <LoginForm
                setUser={setUser} 
                successMessage={successMessage}
                failureMessage={failureMessage}
              />
              
            </div>

        )}
        </div>
        <Projects
          setProjects={setProjects}
          projects={projects}
          setTasks={setTasks}
          tasks={tasks}
          user={user}
          successMessage={successMessage}
          failureMessage={failureMessage}
        />
      </div>
      

    </>
  )
}

export default App
