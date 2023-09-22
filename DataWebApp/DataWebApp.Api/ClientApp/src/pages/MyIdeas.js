import React, { useEffect, useState } from 'react';
import useFetchWithMsal from '../hooks/useFetchWithMsal';
import { loginRequest, protectedResources } from "../authConfig";
import { Avatar, List, Spin, Card } from 'antd';
import { LineChartOutlined, FallOutlined, RiseOutlined } from '@ant-design/icons';
import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import { BASE_URI } from '../config';

const getAvatarIcon = (status) => {
    let defaultIcon = <LineChartOutlined style={{ color: '#FFFFFF' }} />;
    if (status % 2 === 0) {
        defaultIcon = <FallOutlined style={{ back: '#FFFFFF' }} />
    }
    else {
        defaultIcon = <RiseOutlined style={{ color: '#FFFFFF' }} />
    }
    return defaultIcon
}

const getAvatarBackground = (status) => {
    let defaultIcon = { backgroundColor: 'blue' };
    if (status % 2 === 0) {
        defaultIcon = { backgroundColor: 'blue' };
    }
    else {
        defaultIcon = { backgroundColor: 'red' };
    }
    return defaultIcon
}

export const MyIdeas = () => {

    const [Ideas, setIdeas] = useState([])
    const { isLoading, error, execute } = useFetchWithMsal({
        scopes: protectedResources.apiTodoList.scopes.read,
    });

    useEffect(() => {
        let isCancelled = false;
        let endpoint = BASE_URI + "/api/Employee/get";
        execute("GET", endpoint).then((response) => {
            setIdeas(response?.payload);
            console.log("useEffect response : ", response?.payload)
        });

        return () => {
            isCancelled = true;
        }
    }, [execute])

    return (
        <>
            {!isLoading && Ideas && Ideas.length > 0 &&
                <List
                    pagination={{ position: 'bottom', align: 'end', pageSize: 5 }}
                    dataSource={Ideas}
                    renderItem={(item, index) => (
                        <List.Item>
                            <List.Item.Meta
                                avatar={
                                    <Avatar shape="square" style={getAvatarBackground(item.employeeId)} icon={getAvatarIcon(item.employeeId)} />
                                }
                                title={<a href="https://psideas.azurewebsites.net/fetch-data">{item.firstName}</a>}
                                description={item.firstName + ' ' + item.lastName}
                            />
                        </List.Item>
                    )}
                />
            }
            {isLoading &&
                <div className="loding">
                    <Spin tip="Loading" size="large">
                        <div className="content" />
                    </Spin></div>}
        </>
    )
}
