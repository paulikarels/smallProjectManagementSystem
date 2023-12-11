import { useState } from 'react'
import taskService from '../../services/tasks'

const TaskForms = ({ user, setTasks, project }) => {
  const [name, setName] = useState('')
  const [description, setDescription] = useState('')
  const [dueDate, setStartDate] = useState('')
  const [status, setStatus] = useState('')

  const handleNameChange = (event) => {
    setName(event.target.value)
  }
  const handleDescriptionChange = (event) => {
    setDescription(event.target.value)
  }
  const handleDueDateChange = (event) => {
    setStartDate(event.target.value)
  }
  const handleStatusChange = (event) => {
    setStatus(event.target.value)
  }

  const addTask = async  (event) => {
    event.preventDefault()
    const taskObject = {
      taskName: name,
      taskDescription: description,
      status: +status,
      dueDate,
      projectID: project.projectID,
      userID: user.user.userID,
    }
    console.log(taskObject, "taskobject")
    await taskService.create(taskObject)
    const updatedTasks = await taskService.getAll()
    setTasks(updatedTasks)
    setName('')
    setDescription('')
    setStartDate('')
    setStatus('')
  }
  

  return (
    <>
    <div >
      <form onSubmit={addTask} >

          <h2>Create new task</h2>
          <p>name: < input value={name}  name='name' required onChange={handleNameChange} /> </p>
          <p>description: <input value={description} name='description' onChange={handleDescriptionChange}/> </p>
          <p>dueDate: <input type='date' value={dueDate} name='startDate' required onChange={handleDueDateChange} /> </p>
          <p>status: <input type='number' min="0" max="2" value={status} name='endDate' required onChange={handleStatusChange} /></p>
          <button type="submit">add task</button> 
        </form>
        
      </div>
    </>
  )



}

export default  TaskForms