import { useState } from 'react'
import projectService from '../../services/projects'

const Project = ({ project, setProjects }) => {
  const [editable, setEditable] = useState(false)
  const [editedProject, setEditedProject] = useState(project)
  
  const formatDate = (dateString) => {

    if (dateString === "" || dateString === null)
      return false

    const date = new Date(dateString)
    const formattedDate = date.toISOString().split('T')[0];
    return formattedDate
  }

  const removeProject = () => {
    if (window.confirm(`Remove project ${editedProject.projectName}?`)) {
      const { projectID  } = editedProject
      projectService.deleteProject(projectID)
    }
  }

  const handleEdit = () => {
    setEditable(true)
  }

  const handleCancel = () => {
    setEditable(false)
  }

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setEditedProject({ ...editedProject, [name]: value })
  }

  const handleUpdateProject = async () => {
    try {
      const updatedProject = editedProject 
      //console.log(updatedProject)
      const updatedData = await projectService.update(updatedProject.projectID, updatedProject)
      setProjects(updatedData)
    } catch (error) {
      // Handle error if the update fails
    }
  }

  return (
    <div>
      {editable ? (
        <div>
          <h3> Edit project: </h3>
          <div>
            <label >Project Name:</label>
            <input
              type="text"
              name="projectName"
              value={editedProject.projectName}
              onChange={handleInputChange}
              style={{ marginBottom: '8px' }}
            />
          </div>
          <div>
            <label >Project Description:</label>
            <input
              type="text"
              name="projectDescription"
              value={editedProject.projectDescription}
              onChange={handleInputChange}
              style={{ marginBottom: '8px' }}
            />
          </div>
          <div>
            <label >Start Date:</label>
            <input
              type="date"
              name="startDate"
              value={formatDate(editedProject.startDate)}
              onChange={handleInputChange}
              style={{ marginBottom: '8px' }}
            />
          </div>
          <div>
            <label >End Date:</label>
            <input
              type="date"
              name="endDate"
              value={formatDate(editedProject.endDate)}
              onChange={handleInputChange}
              style={{ marginBottom: '8px' }}
            />
          </div>
          <button onClick={handleUpdateProject}>Save</button>
          <button onClick={handleCancel}>Cancel</button>
        </div>
      ) : (

        <div >
          <h1> {project.projectName}</h1>
          <h5> {project.projectDescription} </h5>
          <p>Start Date: {new Date(project.startDate).toLocaleDateString('en-FI')}</p>
          <p>End Date: {new Date(project.endDate).toLocaleDateString('en-FI')}</p>
          <button onClick={handleEdit}>Edit Project</button>  
          <button onClick={removeProject}  >Delete Project</button>
        </div>
        
      )}
      
    </div>
  )
}


export default Project