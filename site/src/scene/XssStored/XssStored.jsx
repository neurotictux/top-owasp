import React, { useState } from 'react'

export function XssStored() {

  const [comments, setComments] = useState([])
  const [comment, setComment] = useState("")



  return (
    <>
      <h4>Xss Stored</h4>
    </>
  )
}