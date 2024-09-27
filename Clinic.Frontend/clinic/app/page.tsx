"use client";

import { Button, Col, Row, Typography, Layout, Form, Input } from 'antd';
import { useState } from 'react';
import Link from 'next/link';

const { Header, Content, Footer } = Layout;
const { Title, Paragraph } = Typography;

export default function Home() {
  const [form] = Form.useForm();
  const [submitted, setSubmitted] = useState(false);

  const handleSubmit = async (values: any) => {
    console.log('Form data:', values);
    setSubmitted(true);
  };

  return (
    <div style={{ minHeight: '100vh' }}>

      <Content style={{ padding: '0 50px', marginTop: 20 }}>
        <div style={{ textAlign: 'center', marginBottom: 40 }}>
          <Title level={1}>Добро пожаловать в нашу клинику</Title>
          <Paragraph>
            Мы предлагаем широкий спектр медицинских услуг, чтобы поддерживать здоровье вас и вашей семьи. Наша команда профессионалов готова предоставить высококачественное обслуживание и индивидуальное лечение.
          </Paragraph>
        </div>

        <Row gutter={16}>
          <Col span={8}>
            <section style={{ border: '1px solid #d9d9d9', borderRadius: '4px', padding: '16px', background: '#fff' }}>
              <Title level={3}>Наши услуги</Title>
              <ul style={{padding: '16px'}}>
                <li>Терапия</li>
                <li>Педиатрия</li>
                <li>Ортопедия</li>
                <li>Кардиология</li>
                <li>Дерматология</li>
              </ul>
              <Button type="primary">
                <Link href="/services">Узнать больше</Link>
              </Button>
            </section>
          </Col>
          <Col span={8}>
            <section style={{ border: '1px solid #d9d9d9', borderRadius: '4px', padding: '16px', background: '#fff' }}>
              <Title level={3}>Контакты</Title>
              <Paragraph>
                Адрес: ул. Здоровья, 123, Городок, HC 45678
              </Paragraph>
              <Paragraph>
                Телефон: (123) 456-7890
              </Paragraph>
              <Paragraph>
                Email: <a href="mailto:info@clinic.com">info@clinic.com</a>
              </Paragraph>
            </section>
          </Col>
          <Col span={8}>
            <section style={{ border: '1px solid #d9d9d9', borderRadius: '4px', padding: '16px', background: '#fff' }}>
              <Title level={3}>Записаться на прием</Title>
              <Form form={form} onFinish={handleSubmit}>
                <Form.Item name="name" rules={[{ required: true, message: 'Пожалуйста, введите ваше имя!' }]}>
                  <Input placeholder="Имя" />
                </Form.Item>
                <Form.Item name="email" rules={[{ required: true, message: 'Пожалуйста, введите ваш email!' }, { type: 'email', message: 'Пожалуйста, введите корректный email!' }]}>
                  <Input placeholder="Email" />
                </Form.Item>
                <Form.Item name="message" rules={[{ required: true, message: 'Пожалуйста, введите ваше сообщение!' }]}>
                  <Input.TextArea placeholder="Сообщение" rows={4} />
                </Form.Item>
                <Form.Item>
                  <Button type="primary" htmlType="submit">Отправить</Button>
                </Form.Item>
              </Form>
              {submitted && <Paragraph>Ваша заявка на прием была отправлена. Мы свяжемся с вами в ближайшее время!</Paragraph>}
            </section>
          </Col>
        </Row>
      </Content>
    </div>
  );
}
