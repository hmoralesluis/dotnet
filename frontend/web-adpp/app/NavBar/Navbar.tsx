import React from 'react'
import Search from './Search'
import Logo from './Logo'


export default function Navbar() {
  return (
    <header 
    className='flex sticky top-0 z-50 justify-between items-center bg-white p-5 text-gray-800 shadow-md
    '>
      <Logo />
      <Search/>
      <div>Login</div>      
    </header>
  )
}
