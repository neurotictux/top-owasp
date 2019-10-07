import React from 'react'
import { Provider } from 'react-redux'
import { HashRouter, Route } from 'react-router-dom'

import { Sidebar, Toolbar } from './components'
import Store from './store'
import { AppContainer, CoreAppContainer, BaseContainer } from './components/styles'
import { ROUTES } from './utils'

import {
  BruteForce,
  Home,
  XssReflected,
  XssStored,
  CommandInjection,
  Csrf,
  FileIncusion,
  FileUpload,
  Login,
  SessionHijaking,
  SqlInjection
} from './scene'

export default () => (
  <Provider store={Store}>
    <AppContainer>
      <Toolbar />
      <CoreAppContainer>
        <HashRouter>
          <Sidebar />
          <BaseContainer>
            <Route path={ROUTES.BRUTE_FORCE} exact={true} component={BruteForce} />
            <Route path={ROUTES.COMMAND_INJECTION} exact={true} component={CommandInjection} />
            <Route path={ROUTES.CSRF} exact={true} component={Csrf} />
            <Route path={ROUTES.FILE_INCLUSION} exact={true} component={FileIncusion} />
            <Route path={ROUTES.FILE_UPLOAD} exact={true} component={FileUpload} />
            <Route path={ROUTES.HOME} exact={true} component={Home} />
            <Route path={ROUTES.LOGIN} exact={true} component={Login} />
            <Route path={ROUTES.SESSION_HIJAKING} exact={true} component={SessionHijaking} />
            <Route path={ROUTES.SQL_INJECTION} exact={true} component={SqlInjection} />
            <Route path={ROUTES.XSS_REFLECTED} exact={true} component={XssReflected} />
            <Route path={ROUTES.XSS_STORED} exact={true} component={XssStored} />
          </BaseContainer>
        </HashRouter>
      </CoreAppContainer>
    </AppContainer>
  </Provider>
)