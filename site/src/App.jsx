import React from 'react'
import { Provider } from 'react-redux'
import { HashRouter, Route } from 'react-router-dom'

import { Sidebar, Toolbar } from './components'
import { Home, XssReflected, XssStored } from './scene'
import Store from './store'
import { AppContainer, CoreAppContainer } from './components/styles'

export default () => (
  <Provider store={Store}>
    <AppContainer>
      <Toolbar />
      <CoreAppContainer>
        <HashRouter>
          <Sidebar />
          <Route path="/" exact={true} component={Home} />
          <Route path="/xss-reflected" exact={true} component={XssReflected} />
          <Route path="/xss-stored" exact={true} component={XssStored} />
        </HashRouter>
      </CoreAppContainer>
    </AppContainer>
  </Provider>
)