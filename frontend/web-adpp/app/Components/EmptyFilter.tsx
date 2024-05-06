'use client'


import React from 'react'
import Heading from './Heading'
import { Button } from 'flowbite-react'
import { useParamsStore } from '../Hooks/useParamsStore'

type Props = {
    title?: string
    subtitle?: string
    showReset?: boolean
    showLogin?: boolean
    callbackUrl?: string
}

export default function EmptyFilter({
    title = 'No matches for this filter',
    subtitle = 'Try changing or resetting the filter',
    showReset,
}: Props) {
    const reset = useParamsStore(state => state.reset);

    return (
        <div className='h-[40vh] flex flex-col gap-2 justify-center items-center shadow-lg'>
            <Heading title={title} subtitle={subtitle} center />
            <div className='mt-4'>
                {showReset && (
                    <button onClick={reset}>Remove Filters</button>
                )}               
            </div>
        </div>
    )
}
