import { useState, useRef } from 'react'
import projectService from './services/projects'
import LoginForm from './components/LoginForm'
import Togglable from './components/Togglable'
import ProjectForm from './components/ProjectForms'

function App() {
  const [projects, setProjects] = useState([])
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
    window.location.reload();
  }

  return (
    <>
      <div>
        <div >
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

      </div>
      

    </>
  )
}

export default App
