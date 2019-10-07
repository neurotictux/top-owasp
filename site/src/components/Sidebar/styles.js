import styled from 'styled-components'
import { Link } from 'react-router-dom'

export const Container = styled.div`
    margin-top: 1px;
    width: 220px;
    background-color: #333;
    display: flex;
    flex-direction: column;
    box-shadow: 5px 5px 5px black;
    & > a:not(:first-child) {
        border-top: solid 1px black;
    }
`

export const MenuItem = styled(Link)`
    font-weight: bold;
    color: white;
    text-transform: uppercase;
    transition: 300ms;
    padding: 10px;
    text-decoration: none;
    &:hover {
        cursor: pointer;
        background-color: #555;
    }
`