import express from 'express';
import axios from 'axios';
import dotenv from 'dotenv';

// טוען את משתני הסביבה מקובץ .env
dotenv.config();

const app = express();
const port = process.env.PORT || 5000;

// בקשת GET שמחזירה את רשימת האפליקציות ב-Render
app.get('/services', async (req, res) => {
  try {
    const response = await axios.get('https://api.render.com/v1/services', {
      headers: {
        Authorization: `Bearer ${process.env.RENDER_API_KEY}`,
      }
    });
    res.json(response.data);  // מחזיר את המידע כ-JSON
  } catch (error) {
    console.error('Error fetching services:', error);
    res.status(500).json({ error: 'Unable to fetch services' });
  }
});

app.listen(port, () => {
  console.log(`Server is running on port ${port}`);
});
