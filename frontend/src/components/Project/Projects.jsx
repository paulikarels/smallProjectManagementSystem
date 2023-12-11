import {useEffect, useRef } from 'react'
import Project from './Project'
import Task from '../Task/Task'
import Togglable from '../Togglable'
import TaskForms from '../Task/TaskForms'
import projectService from '../../services/projects'
import taskService from '../../services/tasks'
import './Projects.css'
const Projects = ({ setProjects, projects, setTasks, tasks, user}) => {

  const taskFormRef = useRef()
  useEffect(() => {
    projectService.getAll().then(projects => setProjects(projects));
    taskService.getAll().then(tasks => setTasks(tasks));
  }, [projectService, taskService, projects, tasks])

  const renderProjectsAndTasks =  () => {

    if (!user || !projects | !tasks) {
      projectService.getAll().then(projects => setProjects(projects))
      return null; 
    }

    return (
      projects
        .filter(project => project.userID === user.user.userID)
        .map(project => (
        <div key={project.projectID} className="project-container">
          <div className="project">
            
            <Project project={project} setProjects={setProjects} />
            <Togglable buttonLabel='new task' ref={taskFormRef}>
              <TaskForms  setTasks={setTasks} user={user} tasks={tasks} project={project} />
            </Togglable>

            <h2>Tasks:</h2> 
            {tasks
              .filter(task => task.projectID === project.projectID )
              .map(task => (
                <div key={task.taskID}>  
                  <Task  task={task} setTasks={setTasks} project={project} user={user} className="task"/>
                </div>
              ))}<br/>
            </div>
        </div> 
      ))
    )
  }

  return (
      <div>
          {renderProjectsAndTasks()}
      </div>  
  )
}

export default Projects