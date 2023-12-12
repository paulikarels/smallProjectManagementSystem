import React from 'react'
import '@testing-library/jest-dom'
import { render, screen } from '@testing-library/react'
import UserLogin from '../components/LoginForm'

test('renders login form', () => {
  const setUser = jest.fn()
  const successMessage = jest.fn()
  const failureMessage = jest.fn()

  render(
    <UserLogin
      setUser={setUser}
      successMessage={successMessage}
      failureMessage={failureMessage}
    />
  )
  expect(screen.getByLabelText('Username:')).toBeInTheDocument()
  expect(screen.getByLabelText('Password:')).toBeInTheDocument()
  expect(screen.getByRole('button', { name: 'Login' })).toBeInTheDocument()
})
