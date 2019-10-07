import styled, { keyframes } from 'styled-components'

export const AppContainer = styled.div`
    background-color: #212121;
`

export const CoreAppContainer = styled.div`
    display: flex;
    flex-direction: row;   
`

export const BaseContainer = styled.div`
    min-height: 100vh;
    display: flex;
    flex-direction: column;
    align-items: center;
    font-size: calc(10px + 2vmin);
    color: white;
    padding-top: 30px;
    padding-bottom: 30px;
    margin-left: 30px;
`