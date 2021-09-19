import { FC } from "react";
import {
    Card,
    CardContent,
    CardMedia,
    IconButton,
    Typography,
} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import React from "react";
import CardFormDialog from "../../FormDialog/CardFormDialog";
import * as Constants from "../../../Constants";

export interface InfoCardData {
    title: string;
    description: string;
    image: string;
    key: number;
}

interface InfoCardProps {
    data: InfoCardData;
    editCard: (card: InfoCardProps["data"]) => void;
}

const InfoCard: FC<InfoCardProps> = ({ data, editCard }) => {
    const [showActions, setShowActions] = React.useState(false);

    const handleMouseEnter = () => {
        setShowActions(true);
    };

    const handleMouseLeave = () => {
        setShowActions(false);
    };

    const [showEditDialog, setShowEditDialog] = React.useState(false);

    const handleClickOpenEditDialog = () => {
        setShowEditDialog(true);
    };

    const handleCloseEditDialog = () => {
        setShowEditDialog(false);
    };

    return (
        <Card
            sx={{ position: "relative" }}
            onMouseEnter={handleMouseEnter}
            onMouseLeave={handleMouseLeave}
        >
            <CardMedia
                component="img"
                image={data.image}
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
                    {data.title}
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
                    {data.description}
                </Typography>
                {showActions && (
                    <IconButton
                        aria-label="edit"
                        sx={{ position: "absolute", top: 5, right: 5 }}
                        onClick={handleClickOpenEditDialog}
                    >
                        <EditIcon />
                    </IconButton>
                )}
                <CardFormDialog
                    open={showEditDialog}
                    handleClose={handleCloseEditDialog}
                    callback={editCard}
                    dialogTitle={Constants.EDIT_CARD}
                    dialogButton={Constants.EDIT}
                    initialCardData={data}
                />
            </CardContent>
        </Card>
    );
};

export default InfoCard;
