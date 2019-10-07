import React from 'react'
import { Container, MenuItem } from './styles'
import { ROUTES } from '../../utils'

export function Sidebar() {
    return (
        <Container>
            <MenuItem to={ROUTES.BRUTE_FORCE}>Brute Force</MenuItem>
            <MenuItem to={ROUTES.COMMAND_INJECTION}>Command Injection</MenuItem>
            <MenuItem to={ROUTES.CSRF}>CSRF</MenuItem>
            <MenuItem to={ROUTES.FILE_INCLUSION}>File Inclusion</MenuItem>
            <MenuItem to={ROUTES.FILE_UPLOAD}>File Upload</MenuItem>
            <MenuItem exact to={ROUTES.HOME}>Home</MenuItem>
            <MenuItem to={ROUTES.LOGIN}>Login</MenuItem>
            <MenuItem to={ROUTES.SESSION_HIJAKING}>Session Hijaking</MenuItem>
            <MenuItem to={ROUTES.SQL_INJECTION}>Sql Injection</MenuItem>
            <MenuItem to={ROUTES.XSS_REFLECTED}>XSS (Reflected)</MenuItem>
            <MenuItem to={ROUTES.XSS_STORED}>XSS (Stored)</MenuItem>
        </Container>
    )
}