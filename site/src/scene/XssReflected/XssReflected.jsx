import React, { useState } from 'react'

import { Title, Input } from './styles'

export function XssReflected() {

  const [comments, setComments] = useState([])
  const [comment, setComment] = useState("")

  function save() {
    if (comment) {
      comments.push(comment)
      setComments(comments)
      setComment('')
    }
    console.log(comments)
  }

  return (
    <>
      <Title>Cross-site Scripting (Reflected)</Title>
      <br />
      <Input value={comment} onChange={val => setComment(val.target.value)} />
      <br />
      <button onClick={() => save()}>Save</button>
      <br />
      <div>
        {comments.map((p, i) => <div key={i + ''}>{p}</div>)}
      </div>

    </>
  )
}