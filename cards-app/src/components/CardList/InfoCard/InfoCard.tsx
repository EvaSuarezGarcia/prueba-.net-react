import { FC } from "react";
import { Card, CardContent, CardMedia, Typography } from "@mui/material";

export interface Props {
    title: string;
    description: string;
    image: string;
    key: number;
}

const InfoCard: FC<Props> = ({ title, description, image, key }) => {
    return (
        <Card key={key} sx={{ position: "relative" }}>
            <CardMedia
                component="img"
                image={image}
                sx={{ height: 200 }}
            ></CardMedia>
            <CardContent sx={{ height: 100 }}>
                <Typography
                    variant="h5"
                    sx={{
                        position: "absolute",
                        bottom: 120,
                        color: "common.white",
                    }}
                >
                    {title}
                </Typography>
                <Typography
                    variant="body2"
                    sx={{
                        overflow: "hidden",
                        textOverflow: "ellipsis",
                        display: "-webkit-box",
                        WebkitLineClamp: 3,
                        WebkitBoxOrient: "vertical",
                    }}
                >
                    {description}
                </Typography>
            </CardContent>
        </Card>
    );
};

export default InfoCard;
