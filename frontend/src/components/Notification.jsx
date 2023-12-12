import React from 'react'

const Notification = ({ message, className = 'success' }) => {

  if (message === null) {
    return null
  }

  return (
    <div className={className}>
      {message}
    </div>
  )
}

export default Notification