import styled from 'styled-components'

export const Container = styled.div`
    min-height: 100vh;
    display: flex;
    flex-direction: column;
    align-items: center;
    font-size: calc(10px + 2vmin);
    color: white;
    padding-top: 30px;
    padding-bottom: 30px;
`

export const Title = styled.h3`
    text-align: center;
`

export const Input = styled.input`
    margin: 10px;
    height: 20px;
    border-radius: 5px;
    min-width: 300px;
`