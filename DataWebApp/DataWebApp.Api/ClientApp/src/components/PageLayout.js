import "../style/lighttheme.css"

import React, { useState } from 'react';
import { Button, Layout, Menu, theme, Card, Switch, Space } from 'antd';
import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react";
import { loginRequest } from "../authConfig";
import { USER_THEMES } from "../config";
import {
    MenuFoldOutlined,
    MenuUnfoldOutlined,
    FormOutlined,
    PlusSquareOutlined,
    UnorderedListOutlined,
    LoginOutlined,
    PoweroffOutlined,
    CheckOutlined,
    CloseOutlined
} from '@ant-design/icons';
import { useNavigate } from "react-router-dom";
import { Link } from 'react-router-dom';
const { Header, Sider, Content } = Layout;


export const PageLayout = (props) => {
    const { IsDarkTheme, setIsDarkTheme } = props;
    const { instance } = useMsal();
    const [collapsed, setCollapsed] = useState(false);
    const { token: { colorBgContainer, logoBackground } } = theme.useToken();

    const navigate = useNavigate();

    const handleLogin = () => {
        instance.loginPopup({
            ...loginRequest,
            redirectUri: '/redirect'
        }).catch((error) => console.log(error));
    }
    const handleLogOutRedirect = () => {
        instance.logoutRedirect({
            account: instance.getActiveAccount(),
        });
    }

    let activeAccount;

    if (instance) {
        activeAccount = instance.getActiveAccount();
    }

    return (
        <Layout>
            <Sider trigger={null} collapsible collapsed={collapsed} style={{
                background: colorBgContainer,
            }}>
                <div className="logo" style={{ background: logoBackground }}>
                    <img src={window.location.origin + '/logo512.png'} alt="logo" />
                </div>
                <Menu
                    theme="dark"
                    mode="inline"
                    defaultSelectedKeys={['1']}
                    onClick={({ key }) => {
                        navigate(key);
                    }}
                    style={{
                        background: colorBgContainer,
                    }}
                    items={[
                        {
                            key: '/createidea',
                            icon: <PlusSquareOutlined />,
                            label: 'Create Idea'
                        },
                        {
                            key: '/',
                            icon: <UnorderedListOutlined />,
                            label: 'My Ideas',
                        },
                        {
                            key: '/reviewideas',
                            icon: <FormOutlined />,
                            label: 'My Review',
                        },
                    ]}
                />
            </Sider>
            <Layout>
                <Header
                    style={{
                        padding: 0,
                        background: colorBgContainer,
                    }}
                    className='header-content'
                >
                    <Button
                        type="text"
                        icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
                        onClick={() => setCollapsed(!collapsed)}
                        style={{
                            fontSize: '16px',
                            width: 64,
                            height: 64,
                        }}
                    />

                    <div className="header-login-content">
                        <div style={{ paddingRight: '8px' }}>
                            <Switch size="small" checked={IsDarkTheme} checkedChildren={USER_THEMES.Dark}
                                unCheckedChildren={USER_THEMES.Light} onChange={(checked) => setIsDarkTheme(checked)} />
                        </div>
                        <AuthenticatedTemplate>
                            <>
                                <p>Hello, {activeAccount ? activeAccount.name : 'Unknown'}!</p>
                                <Button
                                    type="text"
                                    icon={<PoweroffOutlined />}
                                    onClick={handleLogOutRedirect}
                                    style={{
                                        fontSize: '16px',
                                        width: 64,
                                        height: 64,
                                        color: 'red'
                                    }}
                                />
                            </>
                        </AuthenticatedTemplate>
                        <UnauthenticatedTemplate>
                            <Button
                                type="text"
                                icon={<LoginOutlined />}
                                onClick={handleLogin}
                                style={{
                                    fontSize: '16px',
                                    width: 64,
                                    height: 64,
                                    float: 'right'
                                }}
                            />
                        </UnauthenticatedTemplate>
                    </div>
                    
                    

                </Header>

                <Content
                    style={{
                        margin: '24px 16px',
                        padding: 24,
                        minHeight: 'Calc(100vh - 10em)',
                        background: colorBgContainer,
                    }}
                >
                    <AuthenticatedTemplate>
                        {props.children}
                    </AuthenticatedTemplate>
                    <UnauthenticatedTemplate>
                        <Card type="inner" title={<span style={{ color: 'red' }}>Please Login!</span>} style={{ width: '100%' }}>
                            <p>You have not logged in yet. Please <span onClick={handleLogin} style={{ color: 'blue', cursor: 'pointer' }}>login</span> to see your ideas.</p>
                        </Card>
                    </UnauthenticatedTemplate>
                </Content>
            </Layout>
        </Layout>
    )
}
