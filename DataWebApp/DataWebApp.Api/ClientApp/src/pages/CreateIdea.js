import React, { useState } from 'react';
import { Button, Form, Input, notification } from 'antd';
import useFetchWithMsal from '../hooks/useFetchWithMsal';
import { loginRequest, protectedResources } from "../authConfig";
import { BASE_URI } from '../config';

export const CreateIdea = () => {
    const [form] = Form.useForm();
    //const [api, contextHolder] = notification.useNotification();

    const [isSaving, setIsSaving] = useState(false);

    const { isLoading, error, execute } = useFetchWithMsal({
        scopes: protectedResources.apiTodoList.scopes.read,
    });

    const formItemLayout = {
        labelCol: {
            span: 4,
        },
        wrapperCol: {
            span: 14,
        },
    };

    const buttonItemLayout = {
        wrapperCol: {
            span: 14,
            offset: 4,
        },
    }

    const submitForm = () => {
        setIsSaving(true);
        form.validateFields()
            .then(async (formObj) => {
                console.log("Submit Form : ", formObj)
                let endpoint = BASE_URI + "/api/Idea/createidea";;
                let resp = await execute("POST", endpoint, formObj)
                console.log("Idea Id genarated : ", resp?.payload.ideaid);
                setIsSaving(false);
                notification.success({
                    message: 'Idea Created!',
                    description:
                        `Your idea have been created successfully! and idea is ${resp?.payload.ideaid}`,
                });

                form.resetFields();
            })
            .catch((errorInfo) => {
                setIsSaving(false);
                console.error("Error On submitForm : ", errorInfo);
                notification.error({
                    message: 'Error!',
                    description:
                        'Something went wrong while creating your idea!',
                });
            });
    }

    return (
        <Form
            {...formItemLayout}
            layout="horizontal"
            form={form}
            initialValues={{
                layout: "horizontal",
            }}
            style={{
                maxWidth: 800,
            }}
        >
            <Form.Item name="ideaName" label="Idea Name">
                <Input placeholder="enter idea name" />
            </Form.Item>
            <Form.Item name="ideaDescription" label="Idea Description">
                <Input placeholder="enter idea description" />
            </Form.Item>
            <Form.Item {...buttonItemLayout}>
                <Button type="primary" htmlType="submit" loading={isSaving} onClick={submitForm}>Submit</Button>
            </Form.Item>
        </Form>
    )
}
