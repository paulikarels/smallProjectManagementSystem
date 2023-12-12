import { useState  } from 'react'
import taskService from '../../services/tasks'

import '../Project/Projects.css'

const Task = ({ task, setTasks }) => {
  const [editable, setEditable] = useState(false)
  const [editedTask, setEditedTask] = useState(task)

  const formatDate = (dateString) => {
    if (dateString === "" || dateString === null)
      return false
    console.log(dateString)
    const date = new Date(dateString)
    const formattedDate = date.toISOString().split('T')[0];
    return formattedDate
  }

  const statusEmoji = {
    0: '🟨', 
    1: '🟩', 
    2: '🟥', 
  }

  const { color: statusColor } = statusEmoji[task.status] || { color: '' }

  const statusSymbolStyle = {
    color: statusColor
  }

  const removeTask = () => {
    if (window.confirm(`Remove task ${editedTask.taskName}`)) {
      const { taskID  } = editedTask
      taskService.deleteTask(taskID)
    }
  }

  const handleEdit = () => {
    setEditable(true)
  }
  const handleCancel = () => {
    setEditable(false)
  }

  const handleInputChange = (event) => {
    const { name, value } = event.target
    setEditedTask({ ...editedTask, [name]: value })
  }

  const handleUpdateTask = async () => {
    try {
      const updatedTask = editedTask
      //console.log(updatedTask)
      editedTask.status = +editedTask.status
      const updatedData = await taskService.update(updatedTask.taskID, updatedTask)

      setTasks(updatedData)
      setEditable(false)
    } catch (error) {
      // Handle error if the update fails
    }
  }
  return (
    <>
      <div className="task-container" key={task.taskID}>
        {editable ? (
        <div>
          <div>
          <label >Task Name:</label>
            <input
              type="text"
              name="taskName" required
              value={editedTask.taskName}
              onChange={handleInputChange}
            />
          </div>
          <div>
            <label >Task Description:</label>
            <input
              type="text"
              name="taskDescription"
              value={editedTask.taskDescription}
              onChange={handleInputChange}
              style={{ marginBottom: '8px' }}
            />
          </div>
          <div>
            <label >Due Date:</label>
            <input
              type="date"
              name="dueDate" 
              value={formatDate(editedTask.dueDate)}
              onChange={handleInputChange}
              style={{ marginBottom: '8px' }}
            />
          </div>
          <div>
            <label >Status:</label>
            <input
              type="number"
              name="status" required
              value={(editedTask.status)}
              min="0" max="2"
              onChange={handleInputChange}
              style={{ marginBottom: '8px' }}
            />
          </div>
            <button onClick={handleUpdateTask}>Save</button>
            <button onClick={handleCancel}>Cancel</button>
          
        </div>
        ) : (

          <div>
            <h3>{task.taskName}</h3>
            <h5>{task.taskDescription}</h5>
            {
            <div className="task-details">
              <p>Due Date: {new Date(task.dueDate).toLocaleDateString('en-FI')}</p>
              <p className="task-status" data-status={task.status} key={task.taskID}>
                {task.status === 0 ? 'Started' : task.status === 1 ? 'Done' : 'Cancelled'} 
                <span role="img" aria-label="status-emoji" style={statusSymbolStyle}>
                  {statusEmoji[task.status]}
                </span>
              </p>
              
            </div>
            
            }
            <button onClick={handleEdit}>Edit</button>
            <button onClick={removeTask} >Delete</button>
            
          </div>
        )}
      </div>
    </>
  )
  
}


export default Task