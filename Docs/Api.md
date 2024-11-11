# QuikNote API Documentation

- [QuikNote API Documentation](#quiknote-api-documentation)
  - [Authentication](#authentication)
    - [Login Request](#login-request)
    - [Request Body](#request-body)
    - [Login Response](#login-response)
    - [Response Body](#response-body)
    - [Register Request](#register-request)
    - [Request Body](#request-body-1)
    - [Register Response](#register-response)
    - [Response Body](#response-body-1)
  - [Create Note](#create-note)
    - [Create Note Request](#create-note-request)
    - [Request Body](#request-body-2)
    - [Create Note Response](#create-note-response)
    - [Headers](#headers)
    - [Response Body](#response-body-2)
  - [Get Note](#get-note)
    - [Get Note Request](#get-note-request)
    - [Get Note Response](#get-note-response)
    - [Response Body](#response-body-3)
  - [Update Note](#update-note)
    - [Update Note Request](#update-note-request)
    - [Request Body](#request-body-3)
    - [Update Note Response](#update-note-response)
    - [Headers](#headers-1)
  - [Delete Note](#delete-note)
    - [Delete Note Request](#delete-note-request)
    - [Delete Note Response](#delete-note-response)

---

## Authentication

### Login Request

```http
POST /auth/login
```

### Request Body

```json
{
    "email": "user@example.com",
    "password": "securepassword"
}
```

### Login Response

```http
200 OK
```

### Response Body

```json
{
    "token": "your_jwt_token",
    "user": {
        "id": "user-unique-id",
        "email": "user@example.com",
        "name": "User Name",
        "registeredDate": "2024-11-01T09:00:00"
    }
}
```

### Register Request

```http
POST /auth/register
```

### Request Body

```json
{
    "name": "User Name",
    "email": "user@example.com",
    "password": "securepassword"
}
```

### Register Response

```http
201 Created
```

### Response Body

```json
{
    "id": "user-unique-id",
    "email": "user@example.com",
    "name": "User Name",
    "registeredDate": "2024-11-01T09:00:00"
}
```

---

## Create Note

### Create Note Request

```http
POST /notes
Authorization: Bearer your_jwt_token
```

### Request Body

```json
  {
      "title": "Meeting Notes",
      "content": "<p>This is a note with <b>bold text</b> and <i>italic text</i>.</p><ul><li>Bullet 1</li><li>Bullet 2</li></ul>",
      "tags": ["meeting", "work"]
  }
```

### Create Note Response

```http
201 Created
```

### Headers

```yaml
Location: {{host}}/notes/{{id}}
```

### Response Body

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "title": "Meeting Notes",
  "content": "<p>This is a note with <b>bold text</b> and <i>italic text</i>.</p><ul><li>Bullet 1</li><li>Bullet 2</li></ul>",
  "tags": ["meeting", "work"],
  "createdDateTime": "2024-11-03T10:00:00",
  "lastModifiedDateTime": "2024-11-03T10:00:00",
  "userId": "user-unique-id"
}
```

---

## Get Note

### Get Note Request

```http
GET /notes/{{id}}
Authorization: Bearer your_jwt_token
```

### Get Note Response

```http
200 OK
```

### Response Body

```json
{
  "id": "note-unique-id",
  "title": "Meeting Notes",
  "content": "<p>This is a note with <b>bold text</b> and <i>italic text</i>.</p><ul><li>Bullet 1</li><li>Bullet 2</li></ul>",
  "tags": ["meeting", "work"],
  "createdDateTime": "2024-11-03T10:00:00",
  "lastModifiedDateTime": "2024-11-03T10:05:00",
  "userId": "user-unique-id"
}
```

---

## Update Note

### Update Note Request

```http
PUT /notes/{{id}}
Authorization: Bearer your_jwt_token
```

### Request Body

```json
{
  "title": "Updated Meeting Notes",
  "content": "<p>This is an updated note with <b>bold text</b> and <i>italic text</i>.</p><ol><li>Numbered item 1</li><li>Numbered item 2</li></ol>",
  "tags": ["meeting", "updated"]
}
```

### Update Note Response

```http
204 No Content
```

or

```http
201 Created
```

### Headers

```yaml
Location: {{host}}/notes/{{id}}
```

---

## Delete Note

### Delete Note Request

```http
DELETE /notes/{{id}}
Authorization: Bearer your_jwt_token
```

### Delete Note Response

```http
204 No Content
```

---

**Notes:**

1. **Authentication & Security:** All requests to the notes endpoints require a valid JWT token in the Authorization header. Tokens are provided upon login and should be securely stored by the client.
2. **User Ownership:** Each note is associated with a userId to ensure that users can only access, update, or delete their own notes.
3. **Rich Text Content:** The content field in notes supports HTML formatting, allowing users to include bold, italic, lists, and other rich-text features.
4. **Date Tracking:** Each note includes createdDateTime and lastModifiedDateTime fields for better traceability.
5. **Rate Limiting & Expiry** (recommended for production): Consider implementing rate limiting and token expiry mechanisms for enhanced security.
