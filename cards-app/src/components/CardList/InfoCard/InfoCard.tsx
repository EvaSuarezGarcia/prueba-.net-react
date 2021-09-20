import { FC } from "react";
import { Box, Card, CardContent, CardMedia, Typography } from "@mui/material";
import React from "react";
import CardActionDialog from "../../Dialogs/CardActionDialog";
import CardFormDialog from "../../Dialogs/FormDialog/CardFormDialog";
import CardIconButton from "../../Buttons/CardIconButton";
import * as Constants from "../../../Constants";
import "./InfoCard.css";

export interface InfoCardData {
    title: string;
    description: string;
    image: string;
    key: number;
    creationDate: number;
}

interface InfoCardProps {
    data: InfoCardData;
    editCard: (card: InfoCardData) => void;
    deleteCard: (cardKey: InfoCardData["key"]) => void;
}

const InfoCard: FC<InfoCardProps> = ({
    data,
    editCard,
    deleteCard: externalDeleteCard,
}) => {
    const [showEditDialog, setShowEditDialog] = React.useState(false);

    const handleClickOpenEditDialog = () => {
        setShowEditDialog(true);
    };

    const handleCloseEditDialog = () => {
        setShowEditDialog(false);
    };

    const [showDeleteDialog, setShowDeleteDialog] = React.useState(false);

    const handleClickOpenDeleteDialog = () => {
        setShowDeleteDialog(true);
    };

    const handleCloseDeleteDialog = () => {
        setShowDeleteDialog(false);
    };

    const deleteCard = () => {
        externalDeleteCard(data.key);
        handleCloseDeleteDialog();
    };

    return (
        <Card sx={{ position: "relative" }} className="card">
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
                {/* For better cross-browser support, we should probably use a plugin */}
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
                {/* Card action buttons */}
                <Box
                    className="card-actions"
                    sx={{ position: "absolute", top: 5, right: 5 }}
                >
                    <CardIconButton
                        icon="edit"
                        aria-label="edit"
                        onClick={handleClickOpenEditDialog}
                    />
                    <CardIconButton
                        icon="delete"
                        aria-label="delete"
                        onClick={handleClickOpenDeleteDialog}
                    />
                </Box>
                {/* Card dialogs. There has to be a way to do this with a single shared dialog, 
                but I can't make it work */}
                <CardFormDialog
                    open={showEditDialog}
                    handleClose={handleCloseEditDialog}
                    callback={editCard}
                    dialogTitle={Constants.EDIT_CARD}
                    dialogButton={Constants.EDIT}
                    initialCardData={data}
                    clearOnSubmit={false}
                />
                <CardActionDialog
                    open={showDeleteDialog}
                    title={Constants.DELETE_CARD_DIALOG_TITLE}
                    body={Constants.DELETE_CARD_DIALOG_BODY.replace(
                        "{title}",
                        data.title
                    )}
                    actionButton={Constants.DELETE}
                    handleClose={handleCloseDeleteDialog}
                    handleAction={deleteCard}
                />
            </CardContent>
        </Card>
    );
};

export default InfoCard;
