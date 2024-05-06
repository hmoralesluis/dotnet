'use client'
import React from 'react'
import Image from 'next/image'

type Props = {
    imageUrl: string
}

export default function CarImage({imageUrl}: Props) {
    const [isLoading, setLoading] = React.useState(true);

    return (
    <Image 
        src={imageUrl}
        alt='Image'
        fill
        priority
        className={`
            ${isLoading ? 'grayscale blur-2xl scale-110' : 'grayscale-0 blur-0 scale-100'}
            object-cover
            group-hover:opacity-75
            duration-700
            ease-in-out        
        `}
        sizes='(max-width: 768px) 100vw, (max-width: 1200px) 50vw, 25vw'
        onLoad={() => setLoading(false)}
    />
    )
}
