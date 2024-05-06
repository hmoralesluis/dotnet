'use client'
import React from 'react'
import { AiOutlineCar } from 'react-icons/ai'
import { useParamsStore } from '../Hooks/useParamsStore'

export default function Logo() {
    const reset = useParamsStore(state => state.reset);
  return (
    <div className='flex items-center gap-2 text-3xl font-semibold text-red-500 cursor-pointer' onClick={reset}>
        <AiOutlineCar size={32} />  
        <div>Auctions</div>
      </div>
  )
}
