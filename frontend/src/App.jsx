import { useState, useRef } from 'react'
import LoginForm from './components/LoginForm'
import Togglable from './components/Togglable'
import ProjectForm from './components/Project/ProjectForms'
import Projects from './components/Project/Projects'
import './index.css'
function App() {
  const [projects, setProjects] = useState([])
  const [tasks, setTasks] = useState([])
  const [user, setUser] = useState(null)

  const projectFormRef = useRef()

  const projectForm = () => (
    <Togglable buttonLabel='new project' ref={projectFormRef}>
      <ProjectForm  setProjects={setProjects} user={user} />
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
              <h2>{user.user.username} logged in</h2>
              {projectForm()}
              <button onClick={handleLogout}>Logout</button>
            </>
          ) : (
            <LoginForm
              setUser={setUser}
            />
        )}
        </div>
        <Projects
          setProjects={setProjects}
          projects={projects}
          setTasks={setTasks}
          tasks={tasks}
          user={user}
        />
      </div>
      

    </>
  )
}

export default App
