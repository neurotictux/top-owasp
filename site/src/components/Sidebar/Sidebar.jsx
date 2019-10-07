import React from 'react'
import { Container, MenuItem } from './styles'
import { NavLink, Link } from 'react-router-dom'

export function Sidebar() {
    return (
        <Container>

            <MenuItem exact to="/">Home</MenuItem>
            <MenuItem>Login</MenuItem>
            <MenuItem>Brute Force</MenuItem>
            <MenuItem to="/xss-reflected">XSS (Reflected)</MenuItem>
            <MenuItem to="/xss-stored">XSS (Stored)</MenuItem>
            <MenuItem>Sql Injection</MenuItem>
            <MenuItem>CSRF</MenuItem>
            <MenuItem>File Inclusion</MenuItem>
            <MenuItem>File Upload</MenuItem>
            <MenuItem>Command Injection</MenuItem>
            <MenuItem>Session Hijaking</MenuItem>
        </Container>
    )
}