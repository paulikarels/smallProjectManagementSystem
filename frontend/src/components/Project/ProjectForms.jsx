import { useState } from 'react'
import projectService from '../../services/projects'
import './Projects.css'

const ProjectForms = ({  setProjects, user, failureMessage, successMessage  }) => {
  const [name, setName] = useState('')
  const [description, setDescription] = useState('')
  const [startDate, setStartDate] = useState('')
  const [endDate, setEndDate] = useState('')

  const handleNameChange = (event) => {
    setName(event.target.value)
  }
  const handleDescriptionChange = (event) => {
    setDescription(event.target.value)
  }
  const handleStartDateChange = (event) => {
    setStartDate(event.target.value)
  }
  const handleEndDateChange = (event) => {
    setEndDate(event.target.value)
  }

  const addProject = async  (event) => {
    event.preventDefault()
    try {
      const projectObject = {
        projectName: name,
        projectDescription: description,
        startDate,
        endDate,
        userID: user.user.userID
      }
      await projectService.create(projectObject)
      const updatedProjects = await projectService.getAll()
      
      setProjects(updatedProjects)
      successMessage(`Added project successfully`)
      setName('')
      setDescription('')
      setStartDate('')
      setEndDate('')
    } catch (exception) {
      failureMessage('Error adding project')
    }
  }


  return (
    <>
    <div className="project-form-container">
       <form onSubmit={addProject} className="project-form">

          <h2>Create new project</h2>
          <p>name: < input value={name}  name='name' required onChange={handleNameChange}/> </p>
          <p>description: <input value={description} name='description' onChange={handleDescriptionChange}/> </p>
          <p>startDate: <input type='date' value={startDate} name='startDate' required  onChange={handleStartDateChange} /> </p>
          <p> endDate: <input type='date' value={endDate} name='endDate' required onChange={handleEndDateChange} /></p>
          <button type="submit">create</button> 
        </form>
        
      </div>
    </>
  )



}

export default  ProjectForms